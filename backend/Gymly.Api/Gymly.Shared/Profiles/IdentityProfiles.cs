using AutoMapper;

namespace Gymly.Shared.Profiles;

public class IdentityProfiles : Profile
{
    public IdentityProfiles()
    {
        CreateMap<Core.Models.Users.Identity, Responses.Auth.UserResponse>();
    }
}
