using Core.Entities;

using EventsWebApp.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public class ParticipantRepository : IParticipantRepository
{
    private readonly EventsDbContext _context;

    public ParticipantRepository(EventsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId)
    {
        return await _context.Participants
            .Where(p => p.EventId == eventId)
            .ToListAsync();
    }

    public async Task AddParticipantAsync(Participant participant)
    {
        await _context.Participants.AddAsync(participant);
    }

    public async Task RemoveParticipantAsync(int participantId)
    {
        var participant = await _context.Participants.FindAsync(participantId);
        if (participant != null)
        {
            _context.Participants.Remove(participant);
        }
    }

    public async Task<Participant> GetParticipantByIdAsync(int id)
    {
        return await _context.Participants.FindAsync(id);
    }

    public async Task UpdateParticipantAsync(Participant participant)
    {
        _context.Participants.Update(participant);
    }
}
