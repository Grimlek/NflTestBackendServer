using System.Net.Mime;
using AngularTestBackendServer.Core.Models.Filters;
using AngularTestBackendServer.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngularTestBackendServer.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class NflController : ControllerBase
{
    private readonly INflService _nflService;

    public NflController(INflService nflService)
    {
        _nflService = nflService;
    }

    [HttpGet("teams")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetTeamsAsync([FromQuery] Search searchParams)
    {
        var teams = await _nflService.GetTeamsAsync(searchParams);
        return Ok(teams);
    }

    [HttpGet("seasons")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetSeasonsAsync([FromQuery] Search searchParams)
    {
        var seasons = await _nflService.GetSeasonsAsync(searchParams);
        return Ok(seasons);
    }

    [HttpGet("divisions")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetDivisionsAsync()
    {
        var divisions = await _nflService.GetDivisionsAsync();
        return Ok(divisions);
    }

    [HttpGet("weeks")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetWeeksAsync()
    {
        var weeks = await _nflService.GetWeeksAsync();
        return Ok(weeks);
    }
}