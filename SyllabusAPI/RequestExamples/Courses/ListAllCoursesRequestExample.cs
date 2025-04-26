using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Courses;

namespace SyllabusAPI.Example.Courses
{
    public class ListAllCoursesRequestExample : IExamplesProvider<ListAllCoursesRequestApiDTO>
    {
        public ListAllCoursesRequestApiDTO GetExamples()
        {
            return new ListAllCoursesRequestApiDTO
            {
                // Add filtering or pagination properties here when avaible
                // Example: PageNumber = 1, PageSize = 10
            };
        }
    }
}
