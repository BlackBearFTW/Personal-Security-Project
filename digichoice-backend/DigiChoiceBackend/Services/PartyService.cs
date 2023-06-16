using DigiChoiceBackend.Common.Services;
using DigiChoiceBackend.Models;
using DigiChoiceBackend.Persistance;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace DigiChoiceBackend.Services;

public class PartyService : IPartyService
{
    private readonly DataContext _context;
    public PartyService(DataContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Party>>> GetAllParties(string? searchByName)
    {
        if (searchByName is null) return await _context.Parties.ToListAsync();

        return await _context.Parties
            .OrderBy(p => p.PositionNr)
            .Where(p => p.Name.ToLower().Contains(searchByName.ToLower()))
            .ToListAsync();
    }

    public async Task<ErrorOr<Party>> GetBySlug(string slug)
    {
        Party? party = await _context.Parties
            .Where(p => p.Slug.ToLower() == slug.ToLower())
            .Include(p => p.PartyMembers.OrderBy(pm => pm.PositionNr))
            .FirstOrDefaultAsync();

        if (party is null) return Error.NotFound("Party.NotFound", "Party not found.");

        return party;
    }
}