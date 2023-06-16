namespace DigiChoiceBackend.ApiContracts.PartyMember;

public class PartyMemberResponse
{
        public Guid Id { get; set; }
        public int PositionNr { get; set; }
        public string Initials { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string ResidentCity { get; set; }
}