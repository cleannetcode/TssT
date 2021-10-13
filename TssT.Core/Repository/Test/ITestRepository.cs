using System.Collections.Generic;
using System.Threading.Tasks;

namespace TssT.Core.Repository.Test
{
    public interface ITestRepository
    {
        Task<int> InsertAsync(Models.Test.NewTest newTest);
        Task<Models.Test.Test> GetAsync(int testId);
        Task<IList<Models.Test.Test>> GetAllAsync();
        Task UpdateAsync(Models.Test.Test test);
        Task DeleteAsync(int testId);
    }
}