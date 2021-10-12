using System;
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
        
        [HttpGet(nameof(Get))]
        public async Task<Core.Models.Test.Test> Get([FromQuery] int id)
        {
            return await _testService.GetAsync(id);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<BaseCollectionResponse<Core.Models.Test.Test>> GetAll()
        {
            var items = await _testService.GetAllAsync();

            return new BaseCollectionResponse<Core.Models.Test.Test>()
            {
                Items = items, TotalCount = items.Count
            };
        }

        [HttpDelete("{id:int}")]
        public async Task Delete([FromRoute]int id)
        {
            await _testService.DeleteAsync(id);
        }
    }
}