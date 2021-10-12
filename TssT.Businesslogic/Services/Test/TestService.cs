using System;
using System.Threading.Tasks;
using AutoMapper;
using TssT.Core.Errors;
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
        
        public async Task<int> CreateAsync(Core.Models.Test.NewTest test)
        {
            if (test == null)
                throw new ValidationException($"Параметр {nameof(test)} является обязательным");
            
            if (string.IsNullOrWhiteSpace(test.Name) || test.Name.Length is <= 3 or > 200)
                throw new ValidationException($"Параметр {nameof(test.Name)} является обязательным и должен содержать не менее 3 и не более 200 символов");
            
            var entity = _mapper.Map<DataAccess.Entities.Test>(test);
            
            entity.CreatedAt = DateTime.UtcNow;
            
            return await _testRepository.InsertAsync(entity);
        }

        public async Task DeleteAsync(int testId)
        {
            if (testId <= default(int))
                throw new ValidationException($"{nameof(testId)} должен быть больше {default(int)}");

            var entity = await _testRepository.GetAsync(testId);
            entity.DeletedAt = DateTime.UtcNow;
            await _testRepository.UpdateAsync(entity);
        }
    }
}