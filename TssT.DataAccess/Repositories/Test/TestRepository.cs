﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Entities.Test> GetAsync(int id)
        {
            if (id <= default(int))
                throw new ArgumentOutOfRangeException(nameof(id), $"Id теста должен быть больше чем {default(int)}");

            return await _dbContext.Tests.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IList<Entities.Test>> GetAllAsync()
        {
            var entities = _dbContext.Tests.ToListAsync();
            return await entities;
        }

        public async Task UpdateAsync(Entities.Test entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Tests.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= default(int))
                throw new ArgumentOutOfRangeException(nameof(id), $"Id теста должен быть больше чем {default(int)}");

            var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
            
            _dbContext.Tests.Remove(entity);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}