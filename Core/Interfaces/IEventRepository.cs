using EventsWebApp.Core.Entities;

namespace EventsWebApp.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsByCriteriaAsync(string name, DateTime? date, string location);
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> GetEventByNameAsync(string name);
        Task<IEnumerable<Event>> ListAllAsync();
        Task UpdateAsync(Event eventEntity);
        Task UpdateEventAsync(Event eventEntity);
        Task AddEventAsync(Event eventEntity);
        Task DeleteAsync(int id);
        Task<PagedList<Event>> GetPagedEventsAsync(int pageNumber, int pageSize);
    }
}
