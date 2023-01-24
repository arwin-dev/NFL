using ConsoleTables;
using NFL.models;
using NFL.Services.DataParser;

namespace NFL
{
    class Program
    {
		public void BestCoach(List<Coach> Coaches, int Season, string[]colName)
		{
			Coach bestCoach = new Coach("",0,"","",0,0,0,0,"");
			int best = 0;
			foreach (Coach coach in Coaches)
			{
				if(coach.season == Season)
				{
					int Wins = (coach.season_Win - coach.season_Loss) + (coach.playoff_Win - coach.playoff_Loss);
					if (Wins > best)
					{
						best = Wins;
						bestCoach = coach;
					}
				}
			}
			Console.WriteLine("The Best in " + Season + " is:");
			var Table = new ConsoleTable(colName);
			Table.AddRow(bestCoach.coachId,bestCoach.season,bestCoach.firstName,bestCoach.lastName,bestCoach.season_Win,bestCoach.season_Loss,bestCoach.playoff_Win,bestCoach.playoff_Loss,bestCoach.team);
			Table.Write();
			Console.WriteLine();
		}
		public void printTable(string[] colName,List<Coach> ?Coaches = null, List<Team> ?Teams = null)
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

		public void coachSearch(List<Coach> Coaches, List<Team> Teams,Coach coachObj, string[] colName)
		{
			List<Coach> SearchCoach = new List<Coach>();
			foreach (Coach coach in Coaches)
			{
				if(coach.lastName.Equals(coachObj.lastName))
				{
					SearchCoach.Add(coach);
				}
			}
			if(SearchCoach.Count > 0)
			{	
				string[] header = new string[colName.Length + 2];
				for(int i = 0; i < header.Length - 2; i++)
				{
					header[i] = colName[i];
				}
				header[colName.Length] = "Location";
				header[colName.Length + 1] = "Name";

				var Table = new ConsoleTable(header);

				foreach (Coach coach in SearchCoach)
				{
					foreach (Team team in Teams)
					{
						if(coach.team == team.teamId)
						{
							Table.AddRow(coach.coachId,coach.season,coach.firstName,coach.lastName,coach.season_Win,coach.season_Loss,coach.playoff_Win,coach.playoff_Loss,coach.team,team.location,team.name);
						}
					}
				}

				Table.Write();
				Console.WriteLine();
			}	
		}

		public void teamSearch(List<Team> Teams, List<Coach> Coaches,string Location, string[] colName)
		{
			List<Team> SearchTeam = new List<Team>();
			foreach (Team team in Teams)
			{
				if(team.location.Equals(Location))
				{
					SearchTeam.Add(team);
				}
			}
			if(SearchTeam.Count > 0)
			{
				string[] header = new string[colName.Length + 1];
				for(int i = 0; i < header.Length - 1; i++)
				{
					header[i] = colName[i];
				}
				header[colName.Length] = "Name";
				
				var Table = new ConsoleTable(header);

				foreach (Team team in SearchTeam)
				{
					foreach (Coach coach in Coaches)
					{
						if(team.teamId == coach.team)
						{
							Table.AddRow(team.teamId,team.location,team.name,team.league,coach.lastName);
						}
					}
				}

				Table.Write();
				Console.WriteLine();
			}
		}
		public void searchCoaches(List<Coach> Coaches,Coach coach, List<string> Param, List<string> ParamData, string[] header)
		{
			if(Param.Count == 0 || ParamData.Count == 0)
			{
				Console.WriteLine("Empty Parameters");
				return;
			}
			var Table = new ConsoleTable(header);
			foreach (Coach itr in Coaches)
			{
				bool flag = true;
				for(int i =0 ;i < Param.Count;i++)
				{
					if(Param[i].Equals("coachid") && !(ParamData[i].Trim().Equals(itr.coachId.Trim())))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("year") && (int.Parse(ParamData[i]) != itr.season))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("firstname") && !(ParamData[i].Trim().Equals(itr.firstName.Trim())))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("lastname") && !(ParamData[i].Trim().Equals(itr.lastName.Trim())))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("season_win") && (int.Parse(ParamData[i]) != itr.season_Win))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("season_loss") && (int.Parse(ParamData[i]) != itr.season_Loss))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("playoff_win") && (int.Parse(ParamData[i]) != itr.playoff_Win))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("playoff_loss") && (int.Parse(ParamData[i]) != itr.playoff_Loss))
					{
						flag = false;
						break;
					}
					if(Param[i].Equals("team") && !(ParamData[i].Trim().Equals(itr.team.Trim())))
					{
						flag = false;
						break;
					}
				}
				if(flag == true)
				{
					Table.AddRow(itr.coachId,itr.season,itr.firstName,itr.lastName,itr.season_Win,itr.season_Loss,itr.playoff_Win,itr.playoff_Loss,itr.team);
				}	
			}
			Table.Write();
			Console.WriteLine();
			
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
				try
				{
					Console.Write("Enter Command: ");
					string input = Console.ReadLine();
					Console.Write("\n");
					string[] command = input.Split(' ');
					switch (command[0])
					{
						case "load_coaches": case "load_teams":
							string file = command[0].Equals("load_coaches") ? coachFile : teamFile;
							file += command[1];
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

						case "add_team":
							Teams.Add(new Team(command[1],command[2],command[3],char.Parse(command[4])));
							break;

						case "coaches_by_name":
							Coach Newcoach = new Coach();
							if(Coaches.Count == 0 || Teams.Count == 0)
							{
								Console.WriteLine("Please load tables before Search");
								break;
							}
							Newcoach.lastName = command[1].Replace("+"," ");

							pr.coachSearch(Coaches, Teams, Newcoach, coachHeader);
							break;
						case "teams_by_city":
							if(Coaches.Count == 0 || Teams.Count == 0)
							{
								Console.WriteLine("Please load tables before Search");
								break;
							}	
							pr.teamSearch(Teams,Coaches,command[1], teamHeader);
							break;
						case "best_coach":
							pr.BestCoach(Coaches,int.Parse(command[1]),coachHeader);
							break;
						case "search_coaches":
							Coach coach = new Coach();
							List<string> param = new List<string>();
							List<string> ParamData = new List<string>();
							if(Coaches.Count == 0 || Teams.Count == 0)
							{
								Console.WriteLine("Please load tables before Search");
								break;
							}
							for(int i = 1; i < command.Length; i++)
							{
								string[] data = command[i].Split("=");
								param.Add(data[0]);
								ParamData.Add(data[1]);
							}
							pr.searchCoaches(Coaches,coach,param,ParamData,coachHeader);
							break;
						case "--help":
							Console.WriteLine("\nCommands:\n => add_team ID LOCATION NAME LEAGUE - add a new team");
							Console.WriteLine(" => add_coach ID SEASON FIRST_NAME LAST_NAME SEASON_WIN SEASON_LOSS PLAYOFF_WIN PLAYOFF_LOSS TEAM - add new coach data"); 
							Console.WriteLine(" => print_coaches - print a listing of all coaches");
							Console.WriteLine(" => print_teams - print a listing of all teams");
							Console.WriteLine(" => coaches_by_name NAME - list info of coaches with the specified name");
							Console.WriteLine(" => teams_by_city CITY - list the teams in the specified city");
							Console.WriteLine(" => load_coach FILENAME - bulk load of coach info from a file");
							Console.WriteLine(" => load_team FILENAME - bulk load of team info from a file");
							Console.WriteLine(" => best_coach SEASON - print the name of the coach with the most net wins in a specified season");
							Console.WriteLine(" => search_coaches field=VALUE - print the name of the coach satisfying the specified conditions");
							Console.WriteLine(" => exit - quit the program"); 
							break;
						case "exit":
							check = false;
							break;	
						default:
							Console.WriteLine("Invalid Command!! (Enter --help for list of commands)");
							break;
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
            }
        }
    }
}