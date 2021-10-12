using System.Collections.Generic;
using System.Threading.Tasks;

namespace TssT.DataAccess.Repositories.Test
{
    public interface ITestRepository
    {
        Task<int> InsertAsync(Entities.Test entity);
        Task<Entities.Test> GetAsync(int id);
        Task<IList<Entities.Test>> GetAllAsync();
        Task UpdateAsync(Entities.Test entity);
        Task DeleteAsync(int id);
    }
}