using MediatR;
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
    [HttpGet("get-by-user-id")]
    [RoleAuthorize("Seller","Administrator")]
    public async Task<IActionResult> GetByUserId([FromQuery] int userId)
    {
        var outputPort = new GetCreditsByUserIdOutputPort();
        var inputPort = new GetCreditsByUserIdInputPort(userId, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
    [ProducesResponseType(200)]
    [HttpGet("get-by-state-id")]
    [RoleAuthorize("Buyer", "Seller","Administrator")]
    public async Task<IActionResult> GetByStateId([FromQuery] int stateId)
    {
        var outputPort = new GetCreditsByStateIdOutputPort();
        var inputPort = new GetCreditsByStateIdInputPort(stateId, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
    [ProducesResponseType(201)]
    [HttpPost]
    [RoleAuthorize("Seller","Administrator")]
    public async Task<IActionResult> CreateCredit([FromBody] CreateCreditRequestDto createCreditDto)
    {
        var outputPort = new CreateOrUpdateCreditOutputPort();
        var inputPort = new CreateCreditInputPort(createCreditDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return StatusCode(201, response);
    }
    
    [ProducesResponseType(200)]
    [HttpPatch("update-status")]
    [RoleAuthorize("Administrator")]
    public async Task<IActionResult> UpdateCreditStatus([FromBody] UpdateCreditStateDto updateCreditStatusDto)
    {
        var outputPort = new CreateOrUpdateCreditOutputPort();
        var inputPort = new UpdateCreditStateInputPort(updateCreditStatusDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Data;
        return Ok(response);
    }
    
}