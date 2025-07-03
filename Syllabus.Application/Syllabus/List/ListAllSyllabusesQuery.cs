using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Programs;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;
using System.Linq;

namespace Syllabus.Application.Syllabus.List
{
    public record ListAllSyllabusesQuery(ListAllSyllabusesRequestApiDTO Request) : IRequest<ErrorOr<ListAllSyllabusesResponseApiDTO>>;

    public class ListAllSyllabusesQueryHandler : IRequestHandler<ListAllSyllabusesQuery, ErrorOr<ListAllSyllabusesResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public ListAllSyllabusesQueryHandler(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
        }

        public async Task<ErrorOr<ListAllSyllabusesResponseApiDTO>> Handle(ListAllSyllabusesQuery request, CancellationToken cancellationToken)
        {
            var allSyllabuses = await _syllabusRepository.GetAllAsync();
            if (allSyllabuses == null || allSyllabuses.Count == 0)
            {
                return new ListAllSyllabusesResponseApiDTO
                {
                    Syllabuses = new List<SyllabusResponseApiDTO>(),
                    TotalCount = 0,
                    CurrentPage = request.Request.Page,
                    PageSize = request.Request.PageSize,
                    TotalPages = 0,
                    HasNextPage = false,
                    HasPreviousPage = false
                };
            }

            // Apply search filter
            var filteredSyllabuses = allSyllabuses.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Request.SearchTerm))
            {
                var searchTerm = request.Request.SearchTerm.ToLower();
                filteredSyllabuses = filteredSyllabuses.Where(s => 
                    s.Name.ToLower().Contains(searchTerm) ||
                    s.ProgramAcademicYear.Program.Name.ToLower().Contains(searchTerm) ||
                    s.ProgramAcademicYear.Program.Department.Name.ToLower().Contains(searchTerm) ||
                    s.ProgramAcademicYear.AcademicYear.ToLower().Contains(searchTerm)
                );
            }

            // Apply department filter
            if (request.Request.DepartmentId.HasValue)
            {
                filteredSyllabuses = filteredSyllabuses.Where(s => 
                    s.ProgramAcademicYear.Program.DepartmentId == request.Request.DepartmentId.Value
                );
            }

            // Apply program filter
            if (request.Request.ProgramId.HasValue)
            {
                filteredSyllabuses = filteredSyllabuses.Where(s => 
                    s.ProgramAcademicYear.ProgramId == request.Request.ProgramId.Value
                );
            }

            // Apply academic year filter
            if (!string.IsNullOrWhiteSpace(request.Request.AcademicYear))
            {
                filteredSyllabuses = filteredSyllabuses.Where(s => 
                    s.ProgramAcademicYear.AcademicYear == request.Request.AcademicYear
                );
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(request.Request.SortBy))
            {
                filteredSyllabuses = request.Request.SortBy.ToLower() switch
                {
                    "name" => request.Request.SortDirection?.ToLower() == "desc" 
                        ? filteredSyllabuses.OrderByDescending(s => s.Name)
                        : filteredSyllabuses.OrderBy(s => s.Name),
                    "program" => request.Request.SortDirection?.ToLower() == "desc"
                        ? filteredSyllabuses.OrderByDescending(s => s.ProgramAcademicYear.Program.Name)
                        : filteredSyllabuses.OrderBy(s => s.ProgramAcademicYear.Program.Name),
                    "department" => request.Request.SortDirection?.ToLower() == "desc"
                        ? filteredSyllabuses.OrderByDescending(s => s.ProgramAcademicYear.Program.Department.Name)
                        : filteredSyllabuses.OrderBy(s => s.ProgramAcademicYear.Program.Department.Name),
                    "academicyear" => request.Request.SortDirection?.ToLower() == "desc"
                        ? filteredSyllabuses.OrderByDescending(s => s.ProgramAcademicYear.AcademicYear)
                        : filteredSyllabuses.OrderBy(s => s.ProgramAcademicYear.AcademicYear),
                    _ => filteredSyllabuses.OrderBy(s => s.Name)
                };
            }
            else
            {
                // Default sorting by name
                filteredSyllabuses = filteredSyllabuses.OrderBy(s => s.Name);
            }

            var totalCount = filteredSyllabuses.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.Request.PageSize);
            var currentPage = Math.Max(1, Math.Min(request.Request.Page, totalPages));
            var skip = (currentPage - 1) * request.Request.PageSize;

            // Apply pagination
            var pagedSyllabuses = filteredSyllabuses.Skip(skip).Take(request.Request.PageSize).ToList();

            var syllabusDtos = pagedSyllabuses.Select(s => new SyllabusResponseApiDTO
            {
                Id = s.Id,
                Name = s.Name,
                Program = new ProgramResponseApiDTO
                {
                    Id = s.ProgramAcademicYear.Program.Id,
                    Name = s.ProgramAcademicYear.Program.Name,
                    Description = s.ProgramAcademicYear.Program.Description,
                    DepartmentId = s.ProgramAcademicYear.Program.DepartmentId,
                    DepartmentName = s.ProgramAcademicYear.Program.Department.Name,
                    CreatedAt = s.ProgramAcademicYear.Program.CreatedAt,
                    UpdatedAt = s.ProgramAcademicYear.Program.UpdatedAt
                },
                ProgramAcademicYear = new ProgramAcademicYearDTO
                {
                    Id = s.ProgramAcademicYear.Id,
                    AcademicYear = s.ProgramAcademicYear.AcademicYear
                },
                Courses = s.Courses.Select(c => new CourseResponseApiDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Code = c.Code,
                    Year = c.Year,
                    Semester = c.Semester,
                    Credits = c.Credits,
                    LectureHours = c.LectureHours,
                    SeminarHours = c.SeminarHours,
                    LabHours = c.LabHours,
                    PracticeHours = c.PracticeHours,
                    ElectiveGroup = c.ElectiveGroup,
                    AcademicProgram = c.Detail?.AcademicProgram,
                    AcademicYear = c.Detail?.AcademicYear,
                    Language = c.Detail?.Language,
                    CourseTypeLabel = c.Detail?.CourseTypeLabel,
                    EthicsCode = c.Detail?.EthicsCode,
                    ExamMethod = c.Detail?.ExamMethod,
                    TeachingFormat = c.Detail?.TeachingFormat,
                    TeachingPlan = c.Detail?.TeachingPlan,
                    EvaluationBreakdown = c.Detail?.EvaluationBreakdown,
                    Objective = c.Detail?.Objective,
                    KeyConcepts = c.Detail?.KeyConcepts,
                    Prerequisites = c.Detail?.Prerequisites,
                    SkillsAcquired = c.Detail?.SkillsAcquired,
                    CourseResponsible = c.Detail?.CourseResponsible,
                    Topics = c.Detail?.Topics?.Select(t => new TopicResponseApiDTO
                    {
                        Title = t.Title,
                        Hours = t.Hours,
                        Reference = t.Reference
                    }).ToList()
                }).ToList()
            }).ToList();

            return new ListAllSyllabusesResponseApiDTO
            {
                Syllabuses = syllabusDtos,
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PageSize = request.Request.PageSize,
                TotalPages = totalPages,
                HasNextPage = currentPage < totalPages,
                HasPreviousPage = currentPage > 1
            };
        }
    }
}
