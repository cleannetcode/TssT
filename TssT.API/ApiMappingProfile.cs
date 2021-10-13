using AutoMapper;
using TssT.API.Contracts;
using TssT.Core.Models;
using TssT.Core.Models.Test;
using Answer = TssT.API.Contracts.Answer;
using LevelImportance = TssT.API.Contracts.LevelImportance;

namespace TssT.API
{
    public class ApiMappingProfile: Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Answer, Core.Models.Answer>().ReverseMap();
            CreateMap<NewTopicGroup, TopicGroup>().ReverseMap();
            CreateMap<NewTopic, Topic>().ReverseMap();
            CreateMap<LevelImportance, Core.Models.LevelImportance>().ReverseMap();
            CreateMap<NewLevelKnowledge, LevelKnowledge>().ReverseMap();
            CreateMap<DataAccess.Entities.User, User>().ReverseMap();
            CreateMap<DataAccess.Entities.Role, Role>().ReverseMap();

            CreateMap<Contracts.Test.NewTest, NewTest>();
        }
    }
}
