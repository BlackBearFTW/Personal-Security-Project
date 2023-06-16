using DigiChoiceBackend.ApiContracts.Party;
using DigiChoiceBackend.ApiContracts.PartyMember;
using DigiChoiceBackend.Models;

namespace DigiChoiceBackend.Mappings;

public static class ModelToResponseMapper
{
	public static PartyResponse ToPartyResponse(this Party party)
	{
		return new PartyResponse()
		{
			Id = party.Id,
			Abbreviation = party.Abbreviation,
			Name = party.Name,
			PositionNr = party.PositionNr,
			Slug = party.Slug,
		};
	}	
	
	public static PartyResponse ToDetailedPartyResponse(this Party party)
	{
		return new DetailedPartyResponse()
		{
			Id = party.Id,
			Abbreviation = party.Abbreviation,
			Name = party.Name,
			Description = party.Description,
			PositionNr = party.PositionNr,
			Slug = party.Slug,
			PartyMembers = party.PartyMembers.Select(pm => pm.ToPartyMemberResponse()).ToList()
		};
	}

	public static PartyMemberResponse ToPartyMemberResponse(this PartyMember partyMember)
	{
		return new PartyMemberResponse()
		{
			Id = partyMember.Id,
			Firstname = partyMember.Firstname,
			Gender = partyMember.Gender,
			Initials = partyMember.Initials,
			Lastname = partyMember.Lastname,
			PositionNr = partyMember.PositionNr,
			ResidentCity = partyMember.ResidentCity
		};
	}
}