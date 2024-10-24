using Core.Entities;
using Microsoft.EntityFrameworkCore;

public interface IParticipantRepository
{
    Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId);
    Task AddParticipantAsync(Participant participant);
    Task RemoveParticipantAsync(int participantId);
    Task<Participant> GetParticipantByIdAsync(int id);
    Task UpdateParticipantAsync(Participant participant);
   }
