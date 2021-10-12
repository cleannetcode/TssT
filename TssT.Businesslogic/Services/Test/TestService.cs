using System;
using System.Threading.Tasks;
using AutoMapper;
using TssT.DataAccess.Repositories.Test;

namespace TssT.Businesslogic.Services.Test
{
    public class TestService: ITestService
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        
        public TestService(
            IMapper mapper,
            ITestRepository testRepository
            )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
        }
        
        public async Task<int> CreateAsync(Core.Models.Test dto)
        {
            var entity = _mapper.Map<DataAccess.Entities.Test>(dto);
            return await _testRepository.InsertAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _testRepository.DeleteAsync(id);
        }
    }
}