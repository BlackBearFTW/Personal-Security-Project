using System.Security.Claims;
using DigiChoiceBackend.ApiContracts.Vote;
using DigiChoiceBackend.Common.Services;
using DigiChoiceBackend.Models;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;

namespace DigiChoiceBackend.Controllers;

[EnableRateLimiting("fixed")]
[Authorize]
[ApiController]
[Route("votes")]
public class VoteController : Controller
{
    private readonly IVoteService _voteService;
    private readonly IPartyMemberService _partyMemberService;

    public VoteController(IVoteService voteService, IPartyMemberService partyMemberService)
    {
        _voteService = voteService;
        _partyMemberService = partyMemberService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateVoteRequest request)
    {
        /*
         
         This should actually be handled in the create method, 
         since we do not want the create method to be called without checking if a vote exists
         
        ErrorOr<bool> result = _voteService.HasVoted();
        
        if (result.IsError)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
        }

        if (result.Value.Equals(true)) return Problem(statusCode: StatusCodes.Status400BadRequest, title: "You can only vote once!");
        */
        
        Console.WriteLine(User);

        string? currentUserId = User.FindFirstValue("sub");
        
        if (currentUserId is null) return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Something went wrong, please try again later.");

        ErrorOr<Created> result = await _voteService.CreateVote(currentUserId, request.PartyMemberId);
        
        if (result.IsError) return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);

        return Ok(new {Message = "Successfully submitted vote!"});
    }

}