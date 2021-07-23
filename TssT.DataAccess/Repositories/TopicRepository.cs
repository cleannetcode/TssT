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
    /// Класс репозиторий для доступа к данным о топиках
    /// </summary>
    public class TopicRepository : ITopicRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TopicRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Добавляет новый топик в базу данных
        /// </summary>
        /// <param name="topic">Топик</param>
        /// <returns>В случае успешного выполнения вернет идентификатор добавленной записи</returns>
        public async Task<int> AddAsync(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            var entity = _mapper.Map<Entities.Topic>(topic);

            var existsCount = await _context.Topics.CountAsync(x => x.Name.Equals(topic.Name));

            if (existsCount > 0)
                throw new ArgumentException($"Topic with title '{topic.Name}' already exists", nameof(topic));

            var entry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        /// <summary>
        /// Получение всех топиков
        /// </summary>
        /// <returns>Возвращает перечисление топиков</returns>
        public async Task<Topic[]> GetAllAsync()
        {
            var entities = await _context.Topics.ToArrayAsync();
            return _mapper.Map<Topic[]>(entities);
        }

        /// <summary>
        /// Получить топик по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор топика</param>
        /// <returns>В случае успешного выполнения вернет топик</returns>
        public async Task<Topic> GetByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _context.Topics.FirstOrDefaultAsync(x => x.Id.Equals(id));

            return _mapper.Map<Topic>(entity);
        }

        /// <summary>
        /// Обновление топика
        /// </summary>
        /// <param name="topic">Топик</param>
        /// <returns>В случае успешного выполнения возвращает количество затронутых записей в бд</returns>
        public async Task Update(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            var entity = _mapper.Map<Entities.Topic>(topic);

            var existsCount = await _context
                .Topics
                .CountAsync(x => 
                    x.Id != topic.Id 
                    && 
                    x.Name.Equals(topic.Name)
                );

            if (existsCount > 0)
                throw new ArgumentException($"Topic with title '{topic.Name}' already exists", nameof(topic));

            var entry = _context.Update(topic);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет топик по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор топика</param>
        /// <returns>В случае успешного выполнения возвращает количество затронутых записей в бд</returns>
        public async Task RemoveAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));

            var entry = await _context.Topics.FirstOrDefaultAsync(x => x.Id.Equals(id));

            _context.Remove(entry);
            await _context.SaveChangesAsync();
        }
    }
}
