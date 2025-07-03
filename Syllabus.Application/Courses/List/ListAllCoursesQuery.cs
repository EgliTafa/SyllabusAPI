using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;
using System.Linq;

namespace SyllabusApplication.Courses.Queries;

public record ListAllCoursesQuery(ListAllCoursesRequestApiDTO Request) : IRequest<ErrorOr<ListAllCoursesResponseApiDTO>>;

public class ListAllCoursesQueryHandler : IRequestHandler<ListAllCoursesQuery, ErrorOr<ListAllCoursesResponseApiDTO>>
{
    private readonly ICourseRepository _courseRepository;

    public ListAllCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
    }

    public async Task<ErrorOr<ListAllCoursesResponseApiDTO>> Handle(ListAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var allCourses = await _courseRepository.GetAllAsync();
        if (allCourses == null)
        {
            return new ListAllCoursesResponseApiDTO
        {
                Courses = new List<CourseResponseApiDTO>(),
                TotalCount = 0,
                CurrentPage = request.Request.Page,
                PageSize = request.Request.PageSize,
                TotalPages = 0,
                HasNextPage = false,
                HasPreviousPage = false
            };
        }

        // Apply search filter
        var filteredCourses = allCourses.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Request.SearchTerm))
        {
            var searchTerm = request.Request.SearchTerm.ToLower();
            filteredCourses = filteredCourses.Where(c => 
                c.Title.ToLower().Contains(searchTerm) ||
                c.Code.ToLower().Contains(searchTerm) ||
                (c.Detail != null && c.Detail.AcademicProgram != null && c.Detail.AcademicProgram.ToLower().Contains(searchTerm))
            );
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(request.Request.SortBy))
        {
            filteredCourses = request.Request.SortBy.ToLower() switch
            {
                "title" => request.Request.SortDirection?.ToLower() == "desc" 
                    ? filteredCourses.OrderByDescending(c => c.Title)
                    : filteredCourses.OrderBy(c => c.Title),
                "code" => request.Request.SortDirection?.ToLower() == "desc"
                    ? filteredCourses.OrderByDescending(c => c.Code)
                    : filteredCourses.OrderBy(c => c.Code),
                "semester" => request.Request.SortDirection?.ToLower() == "desc"
                    ? filteredCourses.OrderByDescending(c => c.Semester)
                    : filteredCourses.OrderBy(c => c.Semester),
                "credits" => request.Request.SortDirection?.ToLower() == "desc"
                    ? filteredCourses.OrderByDescending(c => c.Credits)
                    : filteredCourses.OrderBy(c => c.Credits),
                "year" => request.Request.SortDirection?.ToLower() == "desc"
                    ? filteredCourses.OrderByDescending(c => c.Year)
                    : filteredCourses.OrderBy(c => c.Year),
                _ => filteredCourses.OrderBy(c => c.Title)
            };
        }
        else
        {
            // Default sorting by title
            filteredCourses = filteredCourses.OrderBy(c => c.Title);
        }

        var totalCount = filteredCourses.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / request.Request.PageSize);
        var currentPage = Math.Max(1, Math.Min(request.Request.Page, totalPages));
        var skip = (currentPage - 1) * request.Request.PageSize;

        // Apply pagination
        var pagedCourses = filteredCourses.Skip(skip).Take(request.Request.PageSize).ToList();

        var courseDtos = pagedCourses.Select(course => new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                Year = course.Year,
                LectureHours = course.LectureHours,
                SeminarHours = course.SeminarHours,
                LabHours = course.LabHours,
                PracticeHours = course.PracticeHours,
                AcademicProgram = course.Detail?.AcademicProgram,
                AcademicYear = course.Detail?.AcademicYear,
                Language = course.Detail?.Language,
                CourseTypeLabel = course.Detail?.CourseTypeLabel,
                EthicsCode = course.Detail?.EthicsCode,
                ExamMethod = course.Detail?.ExamMethod,
                TeachingFormat = course.Detail?.TeachingFormat,
                TeachingPlan = course.Detail?.TeachingPlan,
                EvaluationBreakdown = course.Detail?.EvaluationBreakdown,
                Objective = course.Detail?.Objective,
                KeyConcepts = course.Detail?.KeyConcepts,
                Prerequisites = course.Detail?.Prerequisites,
                SkillsAcquired = course.Detail?.SkillsAcquired,
                CourseResponsible = course.Detail?.CourseResponsible,
                Topics = course.Detail?.Topics?.Select(t => new TopicResponseApiDTO
                {
                    Title = t.Title,
                    Hours = t.Hours,
                    Reference = t.Reference
                }).ToList(),
                ElectiveGroup = course.ElectiveGroup
        }).ToList();

        return new ListAllCoursesResponseApiDTO
        {
            Courses = courseDtos,
            TotalCount = totalCount,
            CurrentPage = currentPage,
            PageSize = request.Request.PageSize,
            TotalPages = totalPages,
            HasNextPage = currentPage < totalPages,
            HasPreviousPage = currentPage > 1
        };
    }
}
