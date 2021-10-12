using System.Threading.Tasks;
using TssT.Core.Models.Test;

namespace TssT.Businesslogic.Services.Test
{
    public interface ITestService
    {
        public Task<int> CreateAsync(NewTest test);
        public Task DeleteAsync(int testId);
    }
}