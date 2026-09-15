using Mcm.Company.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Presentation.Controllers;

[ApiController]
// [Authorize]
[Route("api/dashboard/typeContacts")]
public class CompanyDashBoardController(
    ICompanyDashboardService companyDashboardService)
    : ControllerBase
{
    private readonly ICompanyDashboardService _companyDashboardService = companyDashboardService;

    [HttpGet("stats")]
    public async Task<ActionResult> Get(
        string time,
        DateTime startDate,
        DateTime endDate)
    {
        if (Enum.TryParse<TimeEnum>(time, out TimeEnum timeEnum)){
            await _companyDashboardService.SendStats(
                timeEnum: timeEnum,
                startDate: startDate,
                endDate: endDate
            );
        }
        return Ok();
    }

    [HttpGet("contactCount")]
    public async Task<ActionResult> GetContactCount()
    {
        await _companyDashboardService.SendViewData();
        return Ok();
    }
}