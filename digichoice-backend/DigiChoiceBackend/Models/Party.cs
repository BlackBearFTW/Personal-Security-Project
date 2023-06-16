namespace DigiChoiceBackend.Models;

public class Party
{
    public Guid Id { get; set; }
    public string Slug { get; set; }
    public int PositionNr { get; set; }
    public string Name { get; set; }
    public string? Abbreviation { get; set; }
    public string Description { get; set; }
    public List<PartyMember> PartyMembers { get; set; }
}