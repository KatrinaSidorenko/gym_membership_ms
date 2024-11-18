using AutoMapper;
using Gymly.Core.Models;
using Gymly.Shared.Requests.Payment;

namespace Gymly.Shared.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<CreatePaymentRequest, Payment>()
            .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.EnrollmentId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}
