using AutoMapper;
using TssT.API.Contracts;

namespace TssT.API
{
    public class ApiMappingProfile: Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Answer, Core.Models.Answer>().ReverseMap();
            CreateMap<TopicGroupCreate, Core.Models.TopicGroup>().ReverseMap();
            CreateMap<TopicCreate, Core.Models.Topic>().ReverseMap();
            CreateMap<LevelImportance, Core.Models.LevelImportance>().ReverseMap();
            CreateMap<LevelKnowledge, Core.Models.LevelKnowledge>().ReverseMap();
            CreateMap<DataAccess.Entities.User, Contracts.User>().ReverseMap();
            CreateMap<DataAccess.Entities.Role, Contracts.Role>().ReverseMap();
        }
    }
}