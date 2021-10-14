using System.Collections.Generic;
using System.Threading.Tasks;

namespace TssT.Core.Repository.Test
{
    public interface ITestRepository
    {
        Task<int> InsertAsync(Models.Test newTest);
        Task<Models.Test> GetAsync(int testId);
        Task<IReadOnlyCollection<Models.Test>> GetAllAsync();
        Task UpdateAsync(Models.Test test);
        Task DeleteAsync(int testId);
    }
}