using AutoMapper;
using TssT.Core.Models;

namespace TssT.API.Mappings.Profiles
{
    public class TopicProfile: Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic, DataAccess.Entities.Topic>()
                .ReverseMap();
        }
    }
}
