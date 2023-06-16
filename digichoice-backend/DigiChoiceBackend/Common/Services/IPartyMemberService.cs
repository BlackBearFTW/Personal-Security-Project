using DigiChoiceBackend.Models;
using ErrorOr;

namespace DigiChoiceBackend.Common.Services;

public interface IPartyMemberService
{
    public Task<ErrorOr<PartyMember>> GetById(Guid id);
}