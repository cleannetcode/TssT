using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.Core.Models.Test;

namespace TssT.Businesslogic.Services.Test
{
    public interface ITestService
    {
        Task<int> CreateAsync(NewTest newTest);
        Task DeleteAsync(int testId);
        Task<Core.Models.Test.Test> GetAsync(int id);
        Task<IList<Core.Models.Test.Test>> GetAllAsync();
    }
}