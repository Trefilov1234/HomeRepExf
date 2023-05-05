using System.Collections.Generic;
using System.Threading.Tasks;
using TestServer.Services.DTO;

namespace TestServer.Services.Results
{
    public interface IResultService
    {
        public Task<bool> AddResult(int testId, string login, int result);
        public Task<List<CustomResultDTO>> GetResults(int testId, string login);
    }
}
