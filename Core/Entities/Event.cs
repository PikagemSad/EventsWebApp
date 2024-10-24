using Core.Entities;

namespace EventsWebApp.Core.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int MaxParticipants { get; set; }
        public string? ImagePath { get; set; }

        public List<Participant> Participants { get; set; }
    }
}
