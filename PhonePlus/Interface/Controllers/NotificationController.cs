using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhonePlus.Application.Ports.Notifications;
using PhonePlus.Application.Ports.Notifications.Input;
using PhonePlus.Application.Ports.Notifications.Output;
using PhonePlus.Interface.DTO.Notification;
using PhonePlus.Interface.Filter;

namespace PhonePlus.Interface.Controllers;

[ApiController]
[Route("api/v1/notification")]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
[ProducesResponseType(400)]
public class NotificationController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(200)]
    [RoleAuthorize("Client","Admin")]
    [HttpGet("get-by-user-id")]
    public async Task<IActionResult> GetByUserId([FromQuery] int userId)
    {
        var outputPort = new GetNotificationsByUserIdOutputPort();
        var inputPort = new GetNotificationsByUserIdInputPort(userId, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Content;
        return Ok(response);
    }
    
    [ProducesResponseType(200)]
    [RoleAuthorize("Client","Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequestDto createNotificationDto)
    {
        var outputPort = new CreateNotificationOutputPort();
        var inputPort = new CreateNotificationInputPort(createNotificationDto, outputPort);
        await mediator.Send(inputPort);
        var response = outputPort.Content;
        return Ok(response);
    }
}