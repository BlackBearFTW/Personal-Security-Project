using DigiChoiceBackend.Common.Services;
using DigiChoiceBackend.Models;
using DigiChoiceBackend.Persistance;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace DigiChoiceBackend.Services;

public class VoteService : IVoteService
{
    private readonly DataContext _context;
    private readonly IPartyMemberService _partyMemberService;

    public VoteService(DataContext context, IPartyMemberService partyMemberService)
    {
        _context = context;
        _partyMemberService = partyMemberService;
    }
    
    public async Task<ErrorOr<Created>> CreateVote(string voteIdentifier, Guid partyMemberId)
    {
        if (await HasVoted(voteIdentifier)) return Error.Conflict("Vote.AlreadyExists", "You may only vote once!");
        
        ErrorOr<PartyMember> partyMemberResult = await _partyMemberService.GetById(partyMemberId);

        if (partyMemberResult.IsError) return partyMemberResult.FirstError;

        await _context.Voters.AddAsync(new Voter()
        {
            VoterId = voteIdentifier
        });
        
        await _context.PartyMembers
            .Where(v => v.Id == partyMemberId)
            .ExecuteUpdateAsync(setters => 
                setters.SetProperty(v => v.VoteCount, v => v.VoteCount + 1)
            );

        if (await _context.SaveChangesAsync() > 0) return Result.Created;
        return Error.Unexpected("Vote.Unable", "Unable to create vote. Try again?");
    }

    private async Task<bool> HasVoted(string voteIdentifier)
    {
       return await _context.Voters.AnyAsync(v => v.VoterId == voteIdentifier);
    }
}