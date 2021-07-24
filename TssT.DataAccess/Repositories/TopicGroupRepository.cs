using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    /// <summary>
    /// Класс репозиторий для доступа к данным о группе топиков
    /// </summary>
    public class TopicGroupRepository : ITopicGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TopicGroupRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Добавляет новую группу топиков в базу данных
        /// </summary>
        /// <param name="topicGroup">Группа топиков</param>
        /// <returns>В случае успешного выполнения вернет идентификатор добавленной записи</returns>
        public async Task<int> AddAsync(TopicGroup topicGroup)
        {
            if (topicGroup == null)
                throw new ArgumentNullException(nameof(topicGroup));

            var entity = _mapper.Map<Entities.TopicGroup>(topicGroup);

            var existsCount = await _context.TopicGroups.CountAsync(x => x.Name.Equals(topicGroup.Name));

            if (existsCount > 0)
                throw new ArgumentException($"Topic group with title '{topicGroup.Name}' already exists", nameof(topicGroup));

            var entry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        /// <summary>
        /// Получение всех групп топиков
        /// </summary>
        /// <returns>Возвращает перечисление групп топиков</returns>
        public async Task<TopicGroup[]> GetAllAsync()
        {
            var entities = await _context
                .Topics
                .AsNoTracking()
                .ToArrayAsync();

            return _mapper.Map<TopicGroup[]>(entities);
        }

        /// <summary>
        /// Получить группу топиков по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор группы топиков</param>
        /// <returns>В случае успешного выполнения вернет группа топиков</returns>
        public async Task<TopicGroup> GetByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _context
                .TopicGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return _mapper.Map<TopicGroup>(entity);
        }

        /// <summary>
        /// Обновление группы топиков
        /// </summary>
        /// <param name="topic">Группа топиков</param>
        /// <returns>В случае успешного выполнения возвращает количество затронутых записей в бд</returns>
        public async Task Update(TopicGroup topicGroup)
        {
            if (topicGroup == null)
                throw new ArgumentNullException(nameof(topicGroup));

            var entity = _mapper.Map<Entities.TopicGroup>(topicGroup);

            var existsCount = await _context
                .TopicGroups
                .CountAsync(x => 
                    x.Id != topicGroup.Id 
                    && 
                    x.Name.Equals(topicGroup.Name)
                );

            if (existsCount > 0)
                throw new ArgumentException($"TopicGroup with title '{topicGroup.Name}' already exists", nameof(topicGroup));

            var entry = _context.Update(topicGroup);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет группу топиков по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор группы топиков</param>
        /// <returns>В случае успешного выполнения возвращает количество затронутых записей в бд</returns>
        public async Task RemoveAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entry = await _context
                .TopicGroups
                .Include(x=>x.Topics)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entry?.Topics.Count > 0)
                throw new Exception("Can't remove not empty Topics Group");

            _context.Remove(entry);

            await _context.SaveChangesAsync();
        }
    }
}
