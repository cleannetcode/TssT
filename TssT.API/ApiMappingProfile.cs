﻿using AutoMapper;
using TssT.API.Contracts;
using TssT.API.Contracts.Test;
using TssT.Core.Models;
using Answer = TssT.API.Contracts.Answer;
using LevelImportance = TssT.API.Contracts.LevelImportance;
using Role = TssT.DataAccess.Entities.Role;
using User = TssT.DataAccess.Entities.User;

namespace TssT.API
{
    public class ApiMappingProfile: Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Answer, Core.Models.Answer>().ReverseMap();
            CreateMap<NewTopicGroup, TopicGroup>().ReverseMap();
            CreateMap<NewTopic, Topic>().ReverseMap();
            CreateMap<LevelImportance, Core.Models.LevelImportance>().ReverseMap();
            CreateMap<NewLevelKnowledge, LevelKnowledge>().ReverseMap();
            CreateMap<User, Contracts.User>().ReverseMap();
            CreateMap<Role, Contracts.Role>().ReverseMap();

            CreateMap<NewTest, Test>();
        }
    }
}
