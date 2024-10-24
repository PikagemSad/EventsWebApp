using AutoMapper;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Text.Json;

namespace EventsWebApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }



        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent([FromBody] EventDto eventDto)
        {
            if (eventDto == null)
            {
                return BadRequest("Event is null");
            }

            var eventEntity = _mapper.Map<Event>(eventDto);

            await _eventService.AddEventAsync(eventEntity);

            var createdEventDto = _mapper.Map<EventDto>(eventEntity);
            return CreatedAtAction(nameof(GetEventById), new { id = eventEntity.Id }, createdEventDto);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedEvents = await _eventService.GetPagedEventsAsync(pageNumber, pageSize);
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(pagedEvents.Items); // Маппинг на DTO

            var paginationMetadata = new
            {
                pagedEvents.TotalCount,
                pagedEvents.PageSize,
                pagedEvents.CurrentPage,
                pagedEvents.TotalPages,
                pagedEvents.HasNext,
                pagedEvents.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(eventDtos);
        }


        [HttpPost("upload/{id}")]
        public async Task<ActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{id}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var eventEntity = await _eventService.GetEventByIdAsync(id);
            if (eventEntity == null)
            {
                return NotFound();
            }

            eventEntity.ImagePath = $"/images/{fileName}";
            await _eventService.UpdateEventAsync(eventEntity);

            return Ok(new { Path = eventEntity.ImagePath });
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByCriteria(
           [FromQuery] string? name,
           [FromQuery] DateTime? date,
           [FromQuery] string? location)
        {
            var events = await _eventService.GetEventsByCriteriaAsync(name, date, location);
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(eventDtos);
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<EventDto>> GetEventByName(string name)
        {
            var eventEntity = await _eventService.GetEventByNameAsync(name);
            if (eventEntity == null)
            {
                return NotFound();
            }
            var eventDto = _mapper.Map<EventDto>(eventEntity);
            return Ok(eventDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, EventDto eventDto)
        {
            if (id != eventDto.Id)
                return BadRequest();

            var eventEntity = _mapper.Map<Event>(eventDto);
            await _eventService.UpdateEventAsync(eventEntity);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(int id)
        {
            var eventEntity = await _eventService.GetEventByIdAsync(id);

            if (eventEntity == null)
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventDto>(eventEntity);
            return Ok(eventDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
