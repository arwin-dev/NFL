using ConsoleTables;
using NFL.models;
using NFL.Services.DataParser;

namespace NFL
{
    class Program
    {
		public void printTable(string[] colName,List<Coach> Coaches = null, List<Team> Teams = null)
		{
			var Table = new ConsoleTable(colName);
			if(Coaches != null)
			{
				foreach (Coach coach in Coaches)
				{
					Table.AddRow(coach.coachId,coach.season,coach.firstName,coach.lastName,coach.season_Win,coach.season_Loss,coach.playoff_Win,coach.playoff_Loss,coach.team);
				}
			}
            else
            {
                foreach (Team team in Teams)
                {
                    Table.AddRow(team.teamId,team.location,team.name,team.league);
                }
            }

            Table.Write();
			Console.WriteLine();

		}
        public static void Main(string[] args)
        {
			Program pr = new Program();
            List<Coach> Coaches = new List<Coach>();
            List<Team> Teams = new List<Team>();
			string[] coachHeader = {"Coach ID","Season","Firstname","Lastname","Season Win","Season Loss","Playoff Win","Playoff Loss","Team"};
			string[] teamHeader = {"Team ID","Location","Name","League"};
            string coachFile = "./dataContext/coaches_season.txt";
            string teamFile = "./dataContext/teams.txt";
            DataService.CoachDataParser(Coaches,coachFile);
            var coachTable = new ConsoleTable(coachHeader);
            // foreach (Coach coach in Coaches)
            // {
            //     coachTable.AddRow(coach.coachId,coach.season,coach.firstName,coach.lastName,coach.season_Win,coach.season_Loss,coach.playoff_Win,coach.playoff_Loss,coach.team);
            // }
            // coachTable.Write();
            // Console.WriteLine();
			
            DataService.TeamDataParser(Teams, teamFile);
			
            var teamTable = new ConsoleTable(teamHeader);
            foreach (Team team in Teams)
            {
                teamTable.AddRow(team.teamId,team.location,team.name,team.league);
            }
            //teamTable.Write();
            //Console.WriteLine();

			pr.printTable(coachHeader,null,Teams);
        }
    }
}