using AutoMapper;
using TssT.Core.Models;

namespace TssT.DataAccess
{
    public class DataAccessMappingProfile: Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Answer, Entities.Answer>().ReverseMap();
            CreateMap<Topic, Entities.Topic>().ReverseMap();
            CreateMap<LevelImportance, Entities.LevelImportance>().ReverseMap();
            CreateMap<LevelKnowledge, Entities.LevelKnowledge>().ReverseMap();

            CreateMap<Test, Entities.Test>().ReverseMap();
        }
    }
}