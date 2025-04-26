using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Courses;

namespace SyllabusAPI.Example.Courses
{
    public class DeleteCourseRequestExample : IExamplesProvider<DeleteCourseRequestApiDTO>
    {
        public DeleteCourseRequestApiDTO GetExamples()
        {
            return new DeleteCourseRequestApiDTO
            {
                CourseId = 1
            };
        }
    }
}
