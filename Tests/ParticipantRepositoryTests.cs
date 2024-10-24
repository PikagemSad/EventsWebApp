using Core.Entities;  // ќбратите внимание, что здесь пространство имен "Core.Entities"
using EventsWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Tests.Repositories
{
    public class ParticipantRepositoryTests
    {
        private readonly EventsDbContext _context;
        private readonly ParticipantRepository _repository;

        public ParticipantRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EventsDbContext>()
                .UseInMemoryDatabase(databaseName: "EventsDb")
                .Options;

            _context = new EventsDbContext(options);
            _repository = new ParticipantRepository(_context);
        }

        [Fact]
        public async Task GetParticipantByIdAsync_ReturnsParticipant()
        {
            var participant = new Participant { Id = 1, Name = "John Doe", Email = "john@example.com", EventId = 1 };
            await _repository.AddParticipantAsync(participant);
            await _context.SaveChangesAsync();

            var result = await _repository.GetParticipantByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddParticipantAsync_SavesParticipant()
        {
            var participant = new Participant { Name = "Jane Doe", Email = "jane@example.com", EventId = 2 };

            await _repository.AddParticipantAsync(participant);
            await _context.SaveChangesAsync();

            var savedParticipant = await _context.Participants.FirstOrDefaultAsync(p => p.Email == "jane@example.com");
            Assert.NotNull(savedParticipant);
            Assert.Equal("Jane Doe", savedParticipant.Name);
        }

        [Fact]
        public async Task GetParticipantByIdAsync_ReturnsNull_WhenParticipantDoesNotExist()
        {
            var result = await _repository.GetParticipantByIdAsync(999); 
            Assert.Null(result);
        }

    }
}
