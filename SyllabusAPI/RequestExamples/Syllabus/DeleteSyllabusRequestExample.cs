using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Syllabus;

namespace SyllabusAPI.Example.Syllabus
{
    public class DeleteSyllabusRequestExample : IExamplesProvider<DeleteSyllabusRequestApiDTO>
    {
        public DeleteSyllabusRequestApiDTO GetExamples()
        {
            return new DeleteSyllabusRequestApiDTO
            {
                SyllabusId = 1
            };
        }
    }
}
