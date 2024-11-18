using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Shared.Requests.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    public PaymentsController(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    [HttpGet("{memberId}")]
    public async Task<IActionResult> GetMemberPayments(long memberId, CancellationToken ct)
    {
        var payments = await _paymentRepository.GetMemberPayments(memberId, ct);
        if (!payments.IsSuccessful)
        {
            return BadRequest(payments.Code);
        }

        return Ok(payments.Data);
    }

    [HttpPost] // with a help of trigger
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest payment, CancellationToken ct)
    {
        var mappedPayment = _mapper.Map<CreatePaymentRequest, Payment>(payment);
        var createdPayment = await _paymentRepository.Create(mappedPayment, ct);
        if (!createdPayment.IsSuccessful)
        {
            return BadRequest(createdPayment.Code);
        }

        return Ok();
    }
}
