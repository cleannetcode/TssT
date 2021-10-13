using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TopicRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddAsync(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            var entity = _mapper.Map<Entities.Topic>(topic);
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
            var entities = await _context
                .Topics
                .AsNoTracking()
                .ToArrayAsync();
            return _mapper.Map<Topic[]>(entities);
        }

        /// <summary>
        /// Получить топик по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор топика</param>
        /// <returns>В случае успешного выполнения вернет топик</returns>
        public async Task<Topic> GetByIdAsync(int id)
        {
            if (id <= default(int))
                throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await _context
                .Topics
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

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

            _context.Update(entity);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет топик по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор топика</param>
        /// <returns>В случае успешного выполнения возвращает количество затронутых записей в бд</returns>
        public async Task RemoveAsync(int id)
        {
            _context.Remove(new Topic()
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }
    }
}
