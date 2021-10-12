using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TssT.Businesslogic.Services.Test;
using TssT.Core.Contracts;
using TssT.Core.Contracts.Test;
using TssT.Core.Models.Test;
using NewTest = TssT.Core.Contracts.Test.NewTest;

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

        [HttpPost]
        public async Task<BaseCreateResponse> Create([FromBody] NewTest request)
        {
            var newTest = _mapper.Map<Core.Models.Test.NewTest>(request);

            var createdId = await _testService.CreateAsync(newTest);

            return new BaseCreateResponse()
            {
                Id = createdId
            };
        }

        [HttpDelete("{id:int}")]
        public async Task Delete([FromRoute]int id)
        {
            await _testService.DeleteAsync(id);
        }
    }
}