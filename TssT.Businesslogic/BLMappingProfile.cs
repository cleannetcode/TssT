using AutoMapper;
using TssT.Core.Models;

namespace TssT.Businesslogic
{
    public class BLMappingProfile: Profile
    {
        public BLMappingProfile()
        {
            CreateMap<Test, DataAccess.Entities.Test>().ReverseMap();
        }
    }
}