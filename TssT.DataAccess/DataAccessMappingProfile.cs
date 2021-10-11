using AutoMapper;

namespace TssT.DataAccess
{
    public class DataAccessMappingProfile: Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Core.Models.Answer, Entities.Answer>().ReverseMap();
            CreateMap<Core.Models.Topic, Entities.Topic>().ReverseMap();
            CreateMap<Core.Models.LevelImportance, Entities.LevelImportance>().ReverseMap();
            CreateMap<Core.Models.LevelKnowledge, Entities.LevelKnowledge>().ReverseMap();
        }
    }
}