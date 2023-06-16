using DigiChoiceBackend.ApiContracts.Party;
using DigiChoiceBackend.Common.Services;
using DigiChoiceBackend.Mappings;
using DigiChoiceBackend.Models;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;

namespace DigiChoiceBackend.Controllers;

[EnableRateLimiting("fixed")]
[OutputCache(Duration=1800)]
[ApiController]
[Route("parties")]
public class PartyController : Controller
{
    private readonly IPartyService _partyService;
    
    public PartyController(IPartyService partyService)
    {
        _partyService = partyService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(string? search = null)
    {
        ErrorOr<List<Party>> result = await _partyService.GetAllParties(search);
        
        if (result.IsError)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
        }

        List<PartyResponse> users = result.Value.Select(p => p.ToPartyResponse()).ToList();

        return Ok(new { Results = users });
    }
    
    [HttpGet("{slug}")]
    public async Task<IActionResult> Get(string slug)
    {
        ErrorOr<Party> result = await _partyService.GetBySlug(slug);
        
        if (result.IsError)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
        }

        return Ok(result.Value.ToDetailedPartyResponse());
    }
    
}