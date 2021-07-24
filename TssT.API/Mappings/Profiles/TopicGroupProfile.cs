using AutoMapper;
using TssT.Core.Models;

namespace TssT.API.Mappings.Profiles
{
    public class TopicGroupProfile : Profile
    {
        public TopicGroupProfile()
        {
            CreateMap<TopicGroup, DataAccess.Entities.TopicGroup>()
                .ReverseMap();
        }
    }
}
