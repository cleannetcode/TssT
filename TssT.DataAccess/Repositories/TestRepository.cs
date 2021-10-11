using System;
using System.Threading.Tasks;
using AutoMapper;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Repositories
{
    public class TestRepository
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

        public async Task<int> Insert(Core.Models.Test testDto)
        {
            if (testDto == null)
                throw new ArgumentNullException(nameof(testDto));

            var entity = _mapper.Map<Test>(testDto);

            await _dbContext.Tests.AddAsync(entity);

            return entity.Id;
        }

    }
}