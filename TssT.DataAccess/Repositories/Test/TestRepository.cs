using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TssT.Core.Models.Test;
using TssT.Core.Repository.Test;

namespace TssT.DataAccess.Repositories.Test
{
    public class TestRepository: ITestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TestRepository(
            ApplicationDbContext dbContext,
            IMapper mapper
        )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> InsertAsync(NewTest newTest)
        {
            if (newTest == null)
                throw new ArgumentNullException(nameof(newTest));

            var entity = _mapper.Map<Entities.Test>(newTest);

            await _dbContext.Tests.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Core.Models.Test.Test> GetAsync(int testId)
        {
            if (testId <= default(int))
                throw new ArgumentOutOfRangeException(nameof(testId), $"Id теста должен быть больше чем {default(int)}");

            var entity = await _dbContext.Tests.FirstOrDefaultAsync(x=>x.Id == testId);

            return _mapper.Map<Core.Models.Test.Test>(entity);
        }

        public async Task<IReadOnlyCollection<Core.Models.Test.Test>> GetAllAsync()
        {
            var entities = await _dbContext.Tests.ToListAsync();
            return _mapper.Map<List<Core.Models.Test.Test>>(entities);
        }

        public async Task UpdateAsync(Core.Models.Test.Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test));

            var entity = _mapper.Map<Entities.Test>(test);

            _dbContext.Tests.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int testId)
        {
            if (testId <= default(int))
                throw new ArgumentOutOfRangeException(nameof(testId), $"Id теста должен быть больше чем {default(int)}");

            var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == testId);

            _dbContext.Tests.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}