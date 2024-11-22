using AutoMapper;
using Gymly.Core.Models;
using Gymly.Shared.DTOs;
using Gymly.Shared.Requests.SportClass;
using Gymly.Shared.Responses.SportClass;

namespace Gymly.Shared.Profiles;

public class SportClassProfile : Profile
{
    public SportClassProfile()
    {
        CreateMap<CreateSportClassRequest, SportClass>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.InstructorName))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        CreateMap<ExtendedSportClass, ExtendedSportClassResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClassId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.InstructorName))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.PaidEnrollments, opt => opt.MapFrom(src => src.PaidEnrollments));
    }
}
