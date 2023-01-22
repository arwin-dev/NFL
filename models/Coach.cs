namespace NFL.models
{
    public class Coach
    {
        public string coachId { get; set; } = string.Empty;
        public int season { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = String.Empty;
        public int season_Win { get; set; }
        public int season_Loss { get; set; }
        public int playoff_Win { get; set; }
        public int playoff_Loss { get; set; }
        public string team { get; set; } = string.Empty;

        public Coach(string Id, int date, string fname, string lname, int sWin, int sLoss, int pWin, int pLoss, string teamName)
        {
            coachId = Id;
            season = date;
            firstName = fname;
            lastName = lname;
            season_Win = sWin;
            season_Loss = sLoss;
            playoff_Win = pWin;
            playoff_Loss = pLoss;
            team = teamName;
        }
    }

}