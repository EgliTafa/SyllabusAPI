using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace SyllabusAPI.Example.Courses
{
    public class UpdateCourseRequestExample : IExamplesProvider<UpdateCourseRequestApiDTO>
    {
        public UpdateCourseRequestApiDTO GetExamples()
        {
            return new UpdateCourseRequestApiDTO
            {
                CourseId = 1,
                Title = "Advanced Algorithms - Updated",
                Code = "CS301-UPDATED",
                Semester = 5,
                LectureHours = 50,
                SeminarHours = 20,
                LabHours = 25,
                Credits = 7,
                Evaluation = EvaluationMethod.ContinuousAssessment,
                Type = CourseType.Elective,
            };
        }
    }
}
