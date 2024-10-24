using AutoMapper;
using Core.Entities;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;


    public ParticipantsController(IParticipantRepository participantRepository, IEventRepository eventRepository, IMapper mapper)
    {
        _participantRepository = participantRepository;
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ParticipantDto>> RegisterParticipant([FromBody] ParticipantDto participantDto)
    {
        if (participantDto == null)
        {
            return BadRequest("Participant data is null");
        }

        var participant = _mapper.Map<Participant>(participantDto);

        await _participantRepository.AddParticipantAsync(participant);

        var createdParticipantDto = _mapper.Map<ParticipantDto>(participant);

        return CreatedAtAction(nameof(GetParticipantById), new { id = participant.Id }, createdParticipantDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParticipantDto>> GetParticipantById(int id)
    {
        var participant = await _participantRepository.GetParticipantByIdAsync(id);

        if (participant == null)
        {
            return NotFound();
        }

        var participantDto = _mapper.Map<ParticipantDto>(participant);
        return Ok(participantDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipant(int id)
    {
        try
        {  
            var participant = await _participantRepository.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                return NotFound($"Participant with ID {id} not found."); 
            }

            await _participantRepository.RemoveParticipantAsync(id); 
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); 
        }
    }
}
