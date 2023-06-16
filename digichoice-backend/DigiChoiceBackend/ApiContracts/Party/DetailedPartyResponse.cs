using System.Text.Json.Serialization;
using DigiChoiceBackend.ApiContracts.PartyMember;

namespace DigiChoiceBackend.ApiContracts.Party;

public class DetailedPartyResponse : PartyResponse
{
    [JsonPropertyOrder(99)]
    public string Description { get; set; }
    
    [JsonPropertyOrder(100)]
    public List<PartyMemberResponse> PartyMembers { get; set; }
}