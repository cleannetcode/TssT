using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TssT.Api.Contracts;
using TssT.API.Contracts;
using TssT.Businesslogic.Services.Test;

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

        [Authorize]
        [HttpPost]
        public async Task<BaseCreateResponse> Create([FromBody] NewTest request)
        {
            Console.WriteLine($"POST Create {request.Name}");
            var newTest = _mapper.Map<Core.Models.Test>(request);

            var createdId = await _testService.CreateAsync(newTest);

            return new BaseCreateResponse()
            {
                Id = createdId
            };
        }

        [HttpGet("{id:int}")]
        public async Task<Core.Models.Test> Get([FromRoute] int id)
        {
            return await _testService.GetAsync(id);
        }

        [HttpGet]
        public async Task<BaseCollectionResponse<Core.Models.Test>> GetAll()
        {
            var items = await _testService.GetAllAsync();

            return new BaseCollectionResponse<Core.Models.Test>()
            {
                Items = new ReadOnlyCollection<Core.Models.Test>(items), TotalCount = items.Count
            };
        }

        [HttpDelete("{id:int}")]
        public async Task Delete([FromRoute]int id)
        {
            await _testService.DeleteAsync(id);
        }
    }
}