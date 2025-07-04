using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Syllabus;

namespace SyllabusAPI.Example.Syllabus
{
    public class UpdateSyllabusRequestExample : IExamplesProvider<UpdateSyllabusRequestApiDTO>
    {
        public UpdateSyllabusRequestApiDTO GetExamples()
        {
            return new UpdateSyllabusRequestApiDTO
            {
                SyllabusId = 1,
                Name = "Computer Science Year 1 - Updated"
            };
        }
    }
}
