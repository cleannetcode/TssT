using System.Threading.Tasks;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Repositories.Test
{
    public interface ITestRepository
    {
        public Task<int> InsertAsync(Entities.Test entity);
        public Task DeleteAsync(int id);
    }
}