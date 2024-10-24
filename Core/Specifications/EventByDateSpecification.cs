using EventsWebApp.Core.Entities;

namespace EventsWebApp.Core.Specifications
{
    public class EventByDateSpecification
    {
        public DateTime Date { get; }

        public EventByDateSpecification(DateTime date)
        {
            Date = date;
        }

        public bool IsSatisfiedBy(Event eventEntity)
        {
            return eventEntity.Date.Date == Date.Date;
        }
    }
}
