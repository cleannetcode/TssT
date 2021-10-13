using AutoMapper;
using TssT.Core.Models;
using TssT.Core.Models.Test;
using Test = TssT.DataAccess.Entities.Test;

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

            CreateMap<NewTest, Test>();
            CreateMap<Core.Models.Test.Test, Test>().ReverseMap();
        }
    }
}