using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface ILevelKnowledgeRepository
    {
        public Task<int> AddAsync(LevelKnowledge levelKnoweledge);
    }
}