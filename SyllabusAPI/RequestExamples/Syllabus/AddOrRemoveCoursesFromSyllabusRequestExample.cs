using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Syllabus;
using System.Collections.Generic;

namespace SyllabusAPI.Example.Syllabus
{
    public class AddOrRemoveCoursesFromSyllabusRequestExample : IExamplesProvider<AddOrRemoveCoursesFromSyllabusRequestApiDTO>
    {
        public AddOrRemoveCoursesFromSyllabusRequestApiDTO GetExamples()
        {
            return new AddOrRemoveCoursesFromSyllabusRequestApiDTO
            {
                SyllabusId = 1,
                CourseIdsToAdd = new List<int> { 3, 4 },
                CourseIdsToRemove = new List<int> { 1 }
            };
        }
    }
}
