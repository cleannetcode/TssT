using AutoMapper;
using TssT.API.Contracts;
using TssT.Core.Models;
using TssT.Core.Models.Test;
using Answer = TssT.API.Contracts.Answer;
using LevelImportance = TssT.API.Contracts.LevelImportance;
using NewTest = TssT.Core.Contracts.Test.NewTest;

namespace TssT.API
{
    public class ApiMappingProfile: Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Answer, Core.Models.Answer>().ReverseMap();
            CreateMap<NewTopicGroup, Core.Models.TopicGroup>().ReverseMap();
            CreateMap<NewTopic, Core.Models.Topic>().ReverseMap();
            CreateMap<LevelImportance, Core.Models.LevelImportance>().ReverseMap();
            CreateMap<NewLevelKnowledge, Core.Models.LevelKnowledge>().ReverseMap();
            CreateMap<DataAccess.Entities.User, Contracts.User>().ReverseMap();
            CreateMap<DataAccess.Entities.Role, Contracts.Role>().ReverseMap();

            CreateMap<NewTest, Test>();
        }
    }
}
