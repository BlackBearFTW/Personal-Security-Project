using System.Text.Json.Serialization;
using DigiChoiceBackend.ApiContracts.PartyMember;

namespace DigiChoiceBackend.ApiContracts.Party;

public class PartyResponse
{
    public Guid Id { get; set; }
    public string Slug { get; set; }
    public int PositionNr { get; set; }
    public string Name { get; set; }
    public string? Abbreviation { get; set; }
}