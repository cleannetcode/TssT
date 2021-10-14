using System.Collections.Generic;
using System.Threading.Tasks;

namespace TssT.Businesslogic.Services.Test
{
    public interface ITestService
    {
        Task<int> CreateAsync(Core.Models.Test newTest);
        Task DeleteAsync(int testId);
        Task<Core.Models.Test> GetAsync(int id);
        Task<IList<Core.Models.Test>> GetAllAsync();
    }
}