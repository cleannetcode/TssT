using AutoMapper;

namespace TssT.DataAccess
{
    public class DataAccessMappingProfile: Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Core.Models.User, Entities.User>().ReverseMap();
            CreateMap<Core.Models.Answer, Entities.Answer>().ReverseMap();
            CreateMap<Core.Models.TopicGroup, Entities.TopicGroup>().ReverseMap();
            CreateMap<Core.Models.Topic, Entities.Topic>().ReverseMap();
            CreateMap<Core.Models.LevelImportance, Entities.LevelImportance>().ReverseMap();
            CreateMap<Core.Models.LevelKnowledge, Entities.LevelKnowledge>().ReverseMap();
            CreateMap<Core.Models.User, Entities.User>().ReverseMap();
            CreateMap<Core.Models.Role, Entities.Role>().ReverseMap();
        }
    }
}