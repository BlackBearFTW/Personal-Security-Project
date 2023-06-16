using DigiChoiceBackend.Common.Services;
using DigiChoiceBackend.Models;
using DigiChoiceBackend.Persistance;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace DigiChoiceBackend.Services;

class PartyMemberService : IPartyMemberService
{
    private readonly DataContext _context;
    
    public PartyMemberService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<ErrorOr<PartyMember>> GetById(Guid id)
    {
        PartyMember? partyMember = await _context.PartyMembers
            .FirstOrDefaultAsync(p => p.Id.Equals(id));

        if (partyMember is null) return Error.NotFound("PartyMember.NotFound", "PartyMember not found.");

        return partyMember;
    }
}