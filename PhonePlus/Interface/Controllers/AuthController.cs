using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhonePlus.Application.Ports.Auth;
using PhonePlus.Application.Ports.Auth.Input;
using PhonePlus.Application.Ports.Auth.Output;
using PhonePlus.Interface.DTO.Auth;
using PhonePlus.Interface.Filter;

namespace PhonePlus.Interface.Controllers;

[ApiController]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
[Route("api/v1/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(201)]
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        var outputPort = new SignUpOutputPort();
        var inputPort = new SignUpInputPort(signUpDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Content;
        return StatusCode(201,response);
    }
    
    [ProducesResponseType(200)]
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
    {
        var outputPort = new SignInOutputPort();
        var inputPort = new SignInInputPort(signInDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Content;
        return Ok(response);
    }

    [ProducesResponseType(200)]
    [AllowAnonymous]
    [HttpGet("verify-email/{email}")]
    public async Task<IActionResult> VerifyEmail([FromRoute] string email)
    {
        var outputPort = new EmailVerificationOutPutPort();
        var inputPort = new EmailVerificationInputPort(email, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Content;
        return Ok(response);
    }
    
    [ProducesResponseType(200)]
    [AllowAnonymous]
    [HttpPost("request-email-verification/{email}")]
    public async Task<IActionResult> RequestEmailVerification([FromRoute] string email)
    {
        var outputPort = new EmailVerificationOutPutPort();
        var inputPort = new EmailVerificationRequestInputPort(email, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Content;
        return Ok(response);
    }
    
    
    
    
    
}