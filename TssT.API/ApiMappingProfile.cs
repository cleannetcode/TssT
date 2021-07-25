using AutoMapper;
using TssT.API.Contracts;

namespace TssT.API
{
    public class ApiMappingProfile: Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<NewUser, Core.Models.User>();
            
            CreateMap<User, Core.Models.User>().ReverseMap();
            CreateMap<Answer, Core.Models.Answer>().ReverseMap();
            CreateMap<Group, Core.Models.Group>().ReverseMap();
            CreateMap<Question, Core.Models.Question>().ReverseMap();
            CreateMap<LevelImportance, Core.Models.LevelImportance>().ReverseMap();
            CreateMap<LevelKnowledge, Core.Models.LevelKnowledge>().ReverseMap();
        }
    }
}