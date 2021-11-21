using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proxet.Tournament
{
    public class TeamGenerator
    {
        
        private struct PlayerInfo {
            public String Name;
            public int VehicleClass, WaitTimeSec;
        }
        private List< PlayerInfo> players;
        private PlayerInfo playerInfo;
        public (string[] team1, string[] team2) GenerateTeams(string filePath)
        {
            //read and sort list
            players = new List<PlayerInfo>();
            String[] line;
            using (StreamReader stream = new StreamReader(filePath))
            {
                stream.ReadLine();
                while (stream.Peek() > -1)
                {
                    line = stream.ReadLine().Split('\t');
                    playerInfo.Name = line[0];
                    playerInfo.WaitTimeSec = Int32.Parse(line[1]);
                    playerInfo.VehicleClass = Convert.ToInt32(line[2]);
                    players.Add(playerInfo);
                }
            }
            var sortedUsers = players.OrderByDescending(u => u.WaitTimeSec);
            players = sortedUsers.ToList();

            string[] team1 = new string[9];
            string[] team2 = new string[9];
            byte i = 0;

            var c1 = (from p in players
                      where 1 == p.VehicleClass
                      select p).ToList();
            var c2 = (from p in players
                      where 2 == p.VehicleClass
                      select p).ToList();
            var c3 = (from p in players
                      where 3 == p.VehicleClass
                      select p).ToList();

            while (i != 3)
            {
                team1[i] = c1.First().Name;
                c1.RemoveAt(0);
                team2[i] = c1.First().Name;
                c1.RemoveAt(0);
                i++;
            }
            while (i != 6)
            {
                team1[i] = c2.First().Name;
                c2.RemoveAt(0);
                team2[i] = c2.First().Name;
                c2.RemoveAt(0);
                i++;
            }
            while (i != 9)
            {
                team1[i] = c3.First().Name;
                c3.RemoveAt(0);
                team2[i] = c3.First().Name;
                c3.RemoveAt(0);
                i++;
            }

            return (team1,team2);
        }
     }
}
