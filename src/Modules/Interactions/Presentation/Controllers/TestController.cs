using Mcm.Interactions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Presentation.Controllers;

[ApiController]
[Route("api/test")]
public class TestController(IInteractionNotificationService service)
    : ControllerBase
{
    private readonly IInteractionNotificationService _service = service;

    // [HasAuthorization(PermissionsEnum.View_Company)] 
    [HttpGet]
    public async Task<ActionResult> GetById()
    {
        await _service.CheckAndSendNotification();
        return Ok(DateTime.UtcNow);
    }

}