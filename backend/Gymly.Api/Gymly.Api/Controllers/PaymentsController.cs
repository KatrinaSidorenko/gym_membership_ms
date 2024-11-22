using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Authorize]
public class PaymentsController : BaseController
{
    private readonly IPaymentRepository _paymentRepository;
    public PaymentsController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberPayments(CancellationToken ct)
    {
        var currentMember = CurrentUser;
        var paymentsResult = await _paymentRepository.GetMemberPayments(currentMember.Id, ct);
        if (!paymentsResult.IsSuccessful)
        {
            return BadRequest(paymentsResult);
        }

        return Ok(paymentsResult.Data);
    }

    [HttpPost] // with a help of trigger
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest payment, CancellationToken ct)
    {
        var mappedPayment = Mapper.Map<CreatePaymentRequest, Payment>(payment);
        var createdPayment = await _paymentRepository.Create(mappedPayment, ct);
        if (!createdPayment.IsSuccessful)
        {
            return BadRequest(createdPayment.Code);
        }

        return Ok();
    }
}
