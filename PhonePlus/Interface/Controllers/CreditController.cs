using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhonePlus.Application.Ports.Credits;
using PhonePlus.Application.Ports.Credits.Input;
using PhonePlus.Application.Ports.Credits.Output;
using PhonePlus.Application.Ports.Notifications;
using PhonePlus.Application.Ports.Notifications.Input;
using PhonePlus.Application.Ports.Notifications.Output;
using PhonePlus.Interface.DTO.Credits;
using PhonePlus.Interface.Filter;

namespace PhonePlus.Interface.Controllers;

[ApiController]
[Route("api/v1/credit")]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
[ProducesResponseType(400)]
public class CreditController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(200)]
    [HttpGet("user")]
    [RoleAuthorize("Emisor")]
    public async Task<IActionResult> GetByUserId([FromQuery] int userId)
    {
        var outputPort = new GetCreditsByUserIdOutputPort();
        var inputPort = new GetCreditsByUserIdInputPort(userId, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
 
    
    [ProducesResponseType(200)]
    [HttpGet]
    [RoleAuthorize("Inversionista")]
    public async Task<IActionResult> GetCredits()
    {
        var outputPort = new GetLatestCreditsOutputPort();
        var inputPort = new GetLatestCreditsInputPort(outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
    [ProducesResponseType(201)]
    [HttpPost]
    [RoleAuthorize("Emisor")]
    public async Task<IActionResult> CreateCredit([FromBody] CreateCreditRequestDto createCreditDto)
    {
        var outputPort = new CreateOrUpdateCreditOutputPort();
        var inputPort = new CreateCreditInputPort(createCreditDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return StatusCode(201, response);
    }
    
    [ProducesResponseType(200)]
    [HttpPatch("update")]
    [RoleAuthorize("Emisor")]
    public async Task<IActionResult> UpdateCreditStatus([FromBody] UpdateCreditStateDto updateCreditStatusDto)
    {
        var outputPort = new CreateOrUpdateCreditOutputPort();
        var inputPort = new UpdateCreditStateInputPort(updateCreditStatusDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
    [ProducesResponseType(200)]
    [HttpGet("payment-plan")]
    [RoleAuthorize("Emisor", "Inversionista")]
    public async Task<IActionResult> GetPaymentPlan([FromQuery] int creditId)
    {
        var outputPort = new RequestPaymentPlanOutputPort();
        var inputPort = new RequestPaymentPlanInputPort(creditId, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
}