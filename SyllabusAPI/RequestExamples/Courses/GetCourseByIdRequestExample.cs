using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Courses;

namespace SyllabusAPI.Example.Courses
{
    public class GetCourseByIdRequestExample : IExamplesProvider<GetCourseByIdRequest>
    {
        public GetCourseByIdRequest GetExamples()
        {
            return new GetCourseByIdRequest
            {
                CourseId = 1
            };
        }
    }
}
