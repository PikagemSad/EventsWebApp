using EventsWebApp.Core.Entities;

namespace EventsWebApp.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsByCriteriaAsync(string name, DateTime? date, string location);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> GetEventByNameAsync(string name);
        Task AddEventAsync(Event eventEntity);
        Task UpdateEventAsync(Event eventEntity);
        Task DeleteEventAsync(int id);
        Task<PagedList<Event>> GetPagedEventsAsync(int pageNumber, int pageSize);
    }
}
