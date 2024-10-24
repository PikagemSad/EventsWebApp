using EventsWebApp.Core.Entities;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int EventId { get; set; }


        [JsonIgnore]
        public Event Event { get; set; }
    }
}
