using AutoMapper;
using Gymly.Core.Models;
using Gymly.Shared.Requests.Enrollment;

namespace Gymly.Shared.Profiles;

public class EnrollmentProfile : Profile
{
    public EnrollmentProfile()
    {
        CreateMap<CreateEnrollmentRequest, Enrollment>()
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
            .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
            .ForMember(dest => dest.EnrollmentDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}
