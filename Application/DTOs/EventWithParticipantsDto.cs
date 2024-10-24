namespace EventsWebApp.Application.DTOs

{
    public class EventWithParticipantsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int MaxParticipants { get; set; }

        public List<ParticipantDto>? Participants { get; set; }
    }
}
