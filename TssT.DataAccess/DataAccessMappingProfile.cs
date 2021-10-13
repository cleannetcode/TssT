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

            CreateMap<Core.Models.Test.NewTest, Entities.Test>();
            CreateMap<Core.Models.Test.Test, Entities.Test>().ReverseMap();
        }
    }
}