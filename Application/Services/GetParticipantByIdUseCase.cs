using EventsWebApp.Application.DTOs;
using EventsWebApp.Core.Exceptions;


public class GetParticipantByIdUseCase
{
    private readonly IParticipantRepository _repository;

    public GetParticipantByIdUseCase(IParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<ParticipantDto> ExecuteAsync(int id)
    {
        var participant = await _repository.GetParticipantByIdAsync(id);

        if (participant == null)
        {
            throw new NotFoundException($"Participant with ID {id} not found.");
        }

        return new ParticipantDto
        {
            Id = participant.Id,
            Name = participant.Name,
            Email = participant.Email,
            RegistrationDate = participant.RegistrationDate,
            BirthDate = participant.BirthDate,
        };
    }
}
