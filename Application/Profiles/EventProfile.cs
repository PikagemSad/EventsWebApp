using AutoMapper;
using Core.Entities;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Core.Entities;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants));
        CreateMap<EventDto, Event>();

        CreateMap<Participant, ParticipantDto>();
        CreateMap<ParticipantDto, Participant>();
    }
}
