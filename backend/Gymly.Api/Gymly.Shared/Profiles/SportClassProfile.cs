using AutoMapper;
using Gymly.Core.Models;
using Gymly.Shared.Requests.SportClass;

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
    }
}
