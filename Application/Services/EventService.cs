using EventsWebApp.Application.Interfaces;
using EventsWebApp.Core.Entities;
using EventsWebApp.Core.Interfaces;
using EventsWebApp.Infrastructure.Data;

namespace EventsWebApp.Application.Services
{
    public class EventService : IEventService
    {
        private readonly EventsDbContext _context;
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository, EventsDbContext context)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetEventsByCriteriaAsync(string name, DateTime? date, string location)
        {
            return await _eventRepository.GetEventsByCriteriaAsync(name, date, location);
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.ListAllAsync();
        }


        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }

        public async Task<Event> GetEventByNameAsync(string name)
        {
            return await _eventRepository.GetEventByNameAsync(name);
        }

        public async Task AddEventAsync(Event eventEntity)
        {
            await _eventRepository.AddEventAsync(eventEntity);
        }

        public async Task UpdateEventAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }
        public async Task<PagedList<Event>> GetPagedEventsAsync(int pageNumber, int pageSize)
        {
            return await _eventRepository.GetPagedEventsAsync(pageNumber, pageSize);
        }

    }
}
