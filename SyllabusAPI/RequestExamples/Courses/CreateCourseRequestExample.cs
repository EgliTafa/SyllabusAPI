using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace SyllabusAPI.Example.Courses
{
    public class CreateCourseRequestExample : IExamplesProvider<CreateCourseRequestApiDTO>
    {
        public CreateCourseRequestApiDTO GetExamples()
        {
            return new CreateCourseRequestApiDTO
            {
                Title = "Advanced Algorithms",
                Code = "CS301",
                Semester = 5,
                LectureHours = 45,
                SeminarHours = 15,
                LabHours = 20,
                Credits = 6,
                Evaluation = EvaluationMethod.ContinuousAssessment,
                Type = CourseType.Mandatory,
                SyllabusId = 2
            };
        }
    }
}
