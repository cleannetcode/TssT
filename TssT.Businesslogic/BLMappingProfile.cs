using AutoMapper;

namespace TssT.Businesslogic
{
    public class BLMappingProfile: Profile
    {
        public BLMappingProfile()
        {
            CreateMap<Core.Models.Test.NewTest, DataAccess.Entities.Test>();
            CreateMap<Core.Models.Test.Test, DataAccess.Entities.Test>().ReverseMap();
        }
    }
}