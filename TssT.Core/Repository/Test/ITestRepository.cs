using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.Core.Models.Test;

namespace TssT.Core.Repository.Test
{
    public interface ITestRepository
    {
        Task<int> InsertAsync(NewTest newTest);
        Task<Models.Test.Test> GetAsync(int testId);
        Task<IReadOnlyCollection<Models.Test.Test>> GetAllAsync();
        Task UpdateAsync(Models.Test.Test test);
        Task DeleteAsync(int testId);
    }
}