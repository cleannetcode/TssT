using AutoMapper;
using System;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.DataAccess.Entities;

namespace TssT.DataAccess.Repositories
{
    public class LevelKnowledgeRepository: ILevelKnowledgeRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public LevelKnowledgeRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Добавляет новый вариант ответа / уровня знаний по топику
        /// </summary>
        /// <param name="topicGroup">Уровень знаний топика</param>
        /// <returns>В случае успешного выполнения вернет идентификатор добавленной записи</returns>
        public async Task<int> AddAsync(Core.Models.LevelKnowledge levelKnowledge)
        {
            if (levelKnowledge == null)
                throw new ArgumentNullException(nameof(levelKnowledge));

            var entity = _mapper.Map<LevelKnowledge>(levelKnowledge);
            var entry = await _context.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }
    }
}
