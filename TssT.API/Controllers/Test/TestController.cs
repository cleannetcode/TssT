﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TssT.Businesslogic.Services.Test;
using TssT.Core.Contracts;
using TssT.Core.Contracts.Test;

namespace TssT.API.Controllers.Test
{
    /// <summary>
    /// Тесты
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITestService _testService;
        
        public TestController(
            IMapper mapper,
            ITestService testService
            )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }

        [HttpPost(nameof(Create))]
        public async Task<BaseCreateResponse> Create([FromBody] TestCreateRequest request)
        {
            var dto = _mapper.Map<Core.Models.Test>(request);

            var createdId = await _testService.CreateAsync(dto);

            return new BaseCreateResponse()
            {
                Id = createdId
            };
        }
    }
}