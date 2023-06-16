namespace DigiChoiceBackend.Models;

public class PartyMember
{
    public Guid Id { get; set; }
    public int PositionNr { get; set; }
    public string Initials { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Gender { get; set; }
    public string ResidentCity { get; set; }
    
    public Party Party { get; set; }
    
    public int VoteCount { get; set; }
}