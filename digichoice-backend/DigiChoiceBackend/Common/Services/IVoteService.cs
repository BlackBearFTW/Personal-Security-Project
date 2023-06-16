using DigiChoiceBackend.Models;
using ErrorOr;

namespace DigiChoiceBackend.Common.Services;

public interface IVoteService
{
    public Task<ErrorOr<Created>> CreateVote(string voteIdentifier, Guid partyMemberId);
}