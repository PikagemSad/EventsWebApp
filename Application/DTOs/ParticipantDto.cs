namespace EventsWebApp.Application.DTOs
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int EventId { get; set; }
    }
}
