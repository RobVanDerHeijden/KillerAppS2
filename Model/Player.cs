using System;
using System.Collections.Generic;

namespace Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PlayerLevel { get; set; }
        public decimal Experience { get; set; }
        public int SkillPoints { get; set; }
        public decimal Money { get; set; }
        public decimal Income { get; set; }
        public int Energy { get; set; }
        public string RealName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<Relation> Relations { get; set; }
        public List<Message> Messages { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Hack> Hacks { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Achievement> Achievements { get; set; }
        public string Role { get; set; }


        public Player()
        {

        }
        public Player(int playerId, string username)
        {
            PlayerId = playerId;
            Username = username;
            PlayerLevel = 1;
            Experience = 0;

        }
        public Player(int playerId, string username, int playerLevel, decimal experience)
        {
            PlayerId = playerId;
            Username = username;
            PlayerLevel = playerLevel;
            Experience = experience;
        }
    }
}
