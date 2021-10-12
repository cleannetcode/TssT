using System.Threading.Tasks;

namespace TssT.Businesslogic.Services.Test
{
    public interface ITestService
    {
        public Task<int> CreateAsync(Core.Models.Test dto);
        public Task DeleteAsync(int id);
    }
}