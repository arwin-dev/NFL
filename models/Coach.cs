using System.Text.RegularExpressions;
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
            string check = paramValidator(Id,date,fname,lname,sWin,sLoss,pWin,pLoss,teamName);
            if(check == "OK")
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
            else
            {
                Console.WriteLine("Invalid Entry: " + check);
            }
        }
        public Coach()
        {
            coachId = string.Empty;
            season = 0;
            firstName = string.Empty;
            lastName = string.Empty;
            season_Win = 0;
            season_Loss = 0;
            playoff_Win = 0;
            playoff_Loss = 0;
            team = string.Empty;
        }
        public string paramValidator(string Id, int date, string fname, string lname, int sWin, int sLoss, int pWin, int pLoss, string teamName)
        {
            // ID validation
            return "OK";
        }
    }

}