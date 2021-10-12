using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Repositories.Test
{
    public class TestRepository: ITestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestRepository(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> InsertAsync(Entities.Test entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbContext.Tests.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= default(int))
                throw new ArgumentOutOfRangeException(nameof(id), $"Id must be grater then {default(int)}");

            var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
            
            _dbContext.Tests.Remove(entity);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}