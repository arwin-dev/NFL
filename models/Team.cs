namespace NFL.models
{
    public class Team
    {
        public string teamId { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public string name  { get; set; } = string.Empty;
        public char league { get; set; }

        public Team(string Id, string Location, string Name, char League)
        {
            teamId = Id;
            location = Location;
            name = Name;
            league = League;
        }
    }
}