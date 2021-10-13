﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TssT.Core.Exceptions;
using TssT.Core.Models.Test;
using TssT.Core.Repository.Test;

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

        public async Task<int> CreateAsync(NewTest newTest)
        {
            if (newTest == null)
                throw new ValidationException(nameof(newTest), $"Параметр {nameof(newTest)} является обязательным");

            if (string.IsNullOrWhiteSpace(newTest.Name) || newTest.Name.Length is <= 3 or > 200)
                throw new ValidationException(nameof(newTest.Name), $"Параметр {nameof(newTest.Name)} является обязательным и должен содержать не менее 3 и не более 200 символов");

            newTest.CreatedAt = DateTime.UtcNow;

            return await _testRepository.InsertAsync(newTest);
        }

        public async Task<Core.Models.Test.Test> GetAsync(int id)

        {
            if (id <= default(int))
                throw new ValidationException(nameof(id), $"{nameof(id)} должен быть больше {default(int)}");

            var entity = await _testRepository.GetAsync(id);

            return _mapper.Map<Core.Models.Test.Test>(entity);
        }

        public async Task<IList<Core.Models.Test.Test>> GetAllAsync()
        {
            var entities = await _testRepository.GetAllAsync();

            return _mapper.Map<List<Core.Models.Test.Test>>(entities);
        }

        public async Task DeleteAsync(int testId)
        {
            if (testId <= default(int))
                throw new ValidationException(nameof(testId), $"{nameof(testId)} должен быть больше {default(int)}");

            var test = await _testRepository.GetAsync(testId);

            if (test == null)
                throw new EntityNotFoundException(testId);

            test.DeletedAt = DateTime.UtcNow;

            await _testRepository.UpdateAsync(test);
        }
    }
}