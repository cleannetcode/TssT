using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.Core.Models.Test;

namespace TssT.Businesslogic.Services.Test
{
    public interface ITestService
    {
        public Task<int> CreateAsync(NewTest newTest);
        public Task DeleteAsync(int testId);
        Task<Core.Models.Test.Test> GetAsync(int id);
        Task<IList<Core.Models.Test.Test>> GetAllAsync();
    }
}