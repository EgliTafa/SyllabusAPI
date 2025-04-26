using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Syllabus;

namespace SyllabusAPI.Example.Syllabus
{
    public class GetSyllabusByIdRequestExample : IExamplesProvider<GetSyllabusByIdRequestApiDTO>
    {
        public GetSyllabusByIdRequestApiDTO GetExamples()
        {
            return new GetSyllabusByIdRequestApiDTO
            {
                SyllabusId = 1
            };
        }
    }
}
