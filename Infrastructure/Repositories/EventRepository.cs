using EventsWebApp.Core.Entities;
using EventsWebApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using EventsWebApp.Infrastructure.Data;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventsDbContext _context;

        public EventRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Participants) 
                .ToListAsync();
        }


        public async Task AddEventAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();

        }


        public async Task UpdateEventAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Event>> GetEventsByCriteriaAsync(string name, DateTime? date, string location)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            if (date.HasValue)
            {
                query = query.Where(e => e.Date.Date == date.Value.Date);
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location));
            }

            return await query.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id); 
        }

        public async Task<Event> GetEventByNameAsync(string name)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.Name == name); 
        }
        public async Task<IEnumerable<Event>> ListAllAsync()
        {
            return await _context.Events
                .Include(e => e.Participants) 
                .ToListAsync();
        }

        public async Task UpdateAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<PagedList<Event>> GetPagedEventsAsync(int pageNumber, int pageSize)
        {
            var events = _context.Events
                                 .Include(e => e.Participants) 
                                 .AsQueryable(); 

            var count = await events.CountAsync(); 
            var items = await events
                               .Skip((pageNumber - 1) * pageSize) 
                               .Take(pageSize) 
                               .ToListAsync();

            return new PagedList<Event>(items, count, pageNumber, pageSize);
        }

    }
}
