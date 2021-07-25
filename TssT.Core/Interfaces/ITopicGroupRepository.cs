using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface ITopicGroupRepository
    {
        public Task<int> AddAsync(TopicGroup topicGroup);
        public Task<TopicGroup[]> GetAllAsync();
        public Task<TopicGroup> GetByIdAsync(int id);
        public Task Update(TopicGroup topicGroup);
        public Task RemoveAsync(int id);
    }
}
