using System.Collections.Generic;

namespace Model
{
    public class Game
    {
        public int GameId { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Hack> Hacks { get; set; }
        public IEnumerable<Mission> Missions { get; set; }
        public IEnumerable<Achievement> Achievements { get; set; }
        public IEnumerable<Gang> Gang { get; set; }

        public Game()
        {

        }

        public Game(int gameId, IEnumerable<Player> players, IEnumerable<Skill> skills, IEnumerable<Hack> hacks, IEnumerable<Mission> missions, IEnumerable<Achievement> achievements, IEnumerable<Gang> gang)
        {
            GameId = gameId;
            Players = players;
            Skills = skills;
            Hacks = hacks;
            Missions = missions;
            Achievements = achievements;
            Gang = gang;
        }
    }
}