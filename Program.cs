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
			bool check = true;
			List<String> callStack = new List<string>();
			callStack.Add("test");
			Program pr = new Program();
            List<Coach> Coaches = new List<Coach>();
            List<Team> Teams = new List<Team>();

			string[] coachHeader = {"Coach ID","Season","Firstname","Lastname","Season Win","Season Loss","Playoff Win","Playoff Loss","Team"};
			string[] teamHeader = {"Team ID","Location","Name","League"};

            string coachFile = "./dataContext/";
            string teamFile = "./dataContext/teams.txt";

            //DataService.CoachDataParser(Coaches,coachFile);
            //DataService.TeamDataParser(Teams, teamFile);

            //pr.printTable(coachHeader,Coaches);

            while(check)
            {
                Console.Write("Enter Command: ");
                string input = Console.ReadLine();
                string[] command = input.Split(' ');
				switch (command[0])
				{
					case "load_coaches":
						coachFile += command[1];
						DataService.CoachDataParser(Coaches,coachFile);
						callStack.Add(command[0]);
						break;
					case "print_coaches":
						if(!callStack.Contains("load_coaches"))
						{
							Console.WriteLine("Please load Database before printing!!!");
							break;
						}
						pr.printTable(coachHeader,Coaches);
						callStack.Add(command[0]);
						break;
					case "add_coach":
						Coaches.Add(new Coach(command[1],int.Parse(command[2]),command[3],command[4],int.Parse(command[5]),int.Parse(command[6]),int.Parse(command[7]),int.Parse(command[8]),command[9]));
						break;
					default:
						check = false;
						break;
				}

            }
			
        }
    }
}