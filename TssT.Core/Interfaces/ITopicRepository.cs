using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface ITopicRepository
    {
        public Task<int> AddAsync(Topic topic);
        public Task<Topic[]> GetAllAsync();
        public Task<Topic> GetByIdAsync(int id);
        public Task Update(Topic topic);
        public Task RemoveAsync(int id);
    }
}
