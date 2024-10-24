using AutoMapper;
using EventsWebApp.Application.DTOs;
using Core.Entities;

namespace EventsWebApp.Application.Profiles
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Participant, ParticipantDto>();
            CreateMap<ParticipantDto, Participant>();
        }
    }
}
