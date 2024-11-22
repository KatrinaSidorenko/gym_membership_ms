using AutoMapper;
using Gymly.Core.Models.Users;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMapper _mapper;
    protected IMapper Mapper =>
        _mapper ??= HttpContext.RequestServices.GetService<IMapper>();


    private IIdentityManager _identityManager;
    protected IIdentityManager IdentityManager =>
        _identityManager ??= HttpContext.RequestServices.GetService<IIdentityManager>();

    protected Identity CurrentUser => IdentityManager.GetCurrentUser();

    protected IActionResult ServerError<T>(Result<T> result) => StatusCode(StatusCodes.Status500InternalServerError, result.GetResultResponse());
    
}
