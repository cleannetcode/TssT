using System.Collections.Generic;
using System.Threading.Tasks;

namespace TssT.Businesslogic.Services.Test
{
    public interface ITestService
    {
        public Task<int> CreateAsync(Core.Models.Test dto);
        Task<Core.Models.Test> GetAsync(int id);
        Task<IList<Core.Models.Test>> GetAllAsync();
        public Task DeleteAsync(int id);
    }
}