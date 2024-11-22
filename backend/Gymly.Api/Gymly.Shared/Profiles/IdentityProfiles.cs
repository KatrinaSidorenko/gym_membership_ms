using AutoMapper;
using Gymly.Core.Models.Users;
using Gymly.Shared.Requests.Member;

namespace Gymly.Shared.Profiles;

public class IdentityProfiles : Profile
{
    public IdentityProfiles()
    {
        CreateMap<Core.Models.Users.Identity, Responses.Auth.UserResponse>();
        CreateMap<Identity, CreateMemberRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
