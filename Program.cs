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

		public void coachSearch(List<Coach> Coaches, List<Team> Teams,string Name, string[] colName)
		{
			List<Coach> SearchCoach = new List<Coach>();
			foreach (Coach coach in Coaches)
			{
				if(coach.lastName.Equals(Name))
				{
					SearchCoach.Add(coach);
				}
			}
			if(SearchCoach.Count > 0)
			{	
				colName.Append("Location");
				colName.Append("Name");
				var Table = new ConsoleTable(colName);
				foreach (var item in colName)
				{
					Console.WriteLine(item);
				}
				foreach (Coach coach in SearchCoach)
				{
					
				}
			}	
		}

        public static void Main(string[] args)
        {
			bool check = true;
			Program pr = new Program();
            List<Coach> Coaches = new List<Coach>();
            List<Team> Teams = new List<Team>();

			string[] coachHeader = {"Coach ID","Season","Firstname","Lastname","Season Win","Season Loss","Playoff Win","Playoff Loss","Team"};
			string[] teamHeader = {"Team ID","Location","Name","League"};

            string coachFile = "./dataContext/";
            string teamFile = "./dataContext/";


            while(check)
            {
                Console.Write("Enter Command: ");
                string input = Console.ReadLine();
                string[] command = input.Split(' ');
				switch (command[0])
				{
					case "load_coaches": case "load_teams":
						string file = command[0].Equals("load_coaches") ? coachFile : teamFile;
						file += command[1];
						Console.WriteLine(file);
						if(command[0].Equals("load_coaches"))
						{
							DataService.CoachDataParser(Coaches,file);
						}
						else
						{
							DataService.TeamDataParser(Teams,file);
						}
						break;
					case "print_coaches": case "print_teams":
						if((command[0].Equals("print_coaches") && Coaches.Count == 0) || (command[0].Equals("print_teams") && Teams.Count == 0))
						{
							Console.WriteLine("Please load Database before printing!!!");
							break;
						}
						if(command[0].Equals("print_coaches"))
						{
							pr.printTable(coachHeader,Coaches);
						}
						else
						{
							pr.printTable(teamHeader,null,Teams);
						}		
						break;
					case "add_coach":
						Coaches.Add(new Coach(command[1],int.Parse(command[2]),command[3],command[4],int.Parse(command[5]),int.Parse(command[6]),int.Parse(command[7]),int.Parse(command[8]),command[9]));
						break;
					case "coaches_by_name":
						if(Coaches.Count == 0 || Teams.Count == 0)
						{
							Console.WriteLine("Please load tables before Search");
							break;
						}
						string coachName = command[1].Replace("+"," ");

						pr.coachSearch(Coaches, Teams, coachName, coachHeader);
						break;
					default:
						check = false;
						break;
				}
            }
        }
    }
}