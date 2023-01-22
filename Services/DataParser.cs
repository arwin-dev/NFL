using NFL.models;
using System.IO;

namespace NFL.Services.DataParser
{
    class DataService
    {
        public static void CoachDataParser(List<Coach> Coaches, string file)
        {
            int beg = 0;
            string line;
            StreamReader data = new StreamReader(file);
            while((line = data.ReadLine()) != null)
            {
                if(beg == 0)
                {
                    beg = 1;
                    continue;             
                }
                string[] words = line.Split(',');
                Coaches.Add(new Coach(words[0],int.Parse(words[1]),words[3],words[4],int.Parse(words[5]),int.Parse(words[6]),int.Parse(words[7]),int.Parse(words[8]),words[9]));
            }
        }

        public static void TeamDataParser(List<Team> Teams, string file)
        {
            int beg = 0;
            string line;
            StreamReader data = new StreamReader(file);
            while((line = data.ReadLine()) != null)
            {
                if(beg == 0)
                {
                    beg = 1;
                    continue;             
                }
                string[] words = line.Split(',');
                Teams.Add(new Team(words[0],words[1],words[2],char.Parse(words[3])));
            }

        }
    }
}