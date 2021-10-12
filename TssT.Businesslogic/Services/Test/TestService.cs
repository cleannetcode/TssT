﻿using System;
using System.Collections.Generic;
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
        
        public async Task<int> CreateAsync(Core.Models.Test dto)
        {
            var entity = _mapper.Map<DataAccess.Entities.Test>(dto);
            
            entity.CreatedAt = DateTime.Now;
            
            return await _testRepository.InsertAsync(entity);
        }
        
        public async Task<Core.Models.Test> GetAsync(int id)
        {
            if (id <= default(int))
                throw new ValidationException($"{nameof(id)} должен быть больше {default(int)}");

            var entity = await _testRepository.GetAsync(id);
            
            return _mapper.Map<Core.Models.Test>(entity);
        }

        public async Task<IList<Core.Models.Test>> GetAllAsync()
        {
            var entities = await _testRepository.GetAllAsync();
            
            return _mapper.Map<List<Core.Models.Test>>(entities);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= default(int))
                throw new ValidationException($"{nameof(id)} должен быть больше {default(int)}");

            var entity = await _testRepository.GetAsync(id);
            entity.DeletedAt = DateTime.Now;
            await _testRepository.UpdateAsync(entity);
        }
    }
}