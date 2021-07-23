﻿using AutoMapper;

namespace TssT.DataAccess
{
    public class DataAccessMappingProfile: Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Core.Models.User, Entities.User>().ReverseMap();
            CreateMap<Core.Models.Answer, Entities.Answer>().ReverseMap();
            CreateMap<Core.Models.Group, Entities.Group>().ReverseMap();
            CreateMap<Core.Models.Question, Entities.Question>().ReverseMap();
            CreateMap<Core.Models.LevelImportance, Entities.LevelImportance>().ReverseMap();
            CreateMap<Core.Models.LevelKnowledge, Entities.LevelKnowledge>().ReverseMap();
        }
    }
}