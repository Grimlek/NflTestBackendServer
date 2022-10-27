using System.Net.Mime;
using AngularTestBackendServer.Core.Models.Filters;
using AngularTestBackendServer.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngularTestBackendServer.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("teams")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> SearchTeamsAsync([FromQuery] Search searchParams)
    {
        var results = await _searchService.SearchTeamsAsync(searchParams);
        return Ok(results);
    }

    [HttpGet("seasons")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> SearchSeasonsAsync([FromQuery] Search searchParams)
    {
        var results = await _searchService.SearchSeasonsAsync(searchParams);
        return Ok(results);
    }
}
