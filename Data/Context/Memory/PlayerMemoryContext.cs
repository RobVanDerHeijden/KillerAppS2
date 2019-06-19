using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    public class PlayerMemoryContext : IPlayerContext
    {
        private List<Player> _players = new List<Player>
        {
            new Player
            {
                PlayerId = 1,
                Username = "UnitPLayer",
                Password = "1234",
                Role = "player",
                PlayerLevel = 0,
                Experience = 0,
                SkillPoints = 5,
                Money = 100,
                Income = 1,
                Energy = 100,
                EnergyRegen = 5,
                RefillableEnergy = 100,
                LastTimeEnergyRefilled = DateTime.Now,
                MaxEnergy = 100,
                RealName = "Unit Player",
                Country = "Netherlands",
                City = "Eindhoven"
            },
            new Player
            {
                PlayerId = 2,
                Username = "UnitPLayer2",
                Password = "1234",
                Role = "player",
                PlayerLevel = 0,
                Experience = 0,
                SkillPoints = 5,
                Money = 100,
                Income = 1,
                Energy = 100,
                EnergyRegen = 5,
                RefillableEnergy = 100,
                LastTimeEnergyRefilled = DateTime.Now,
                MaxEnergy = 100,
                RealName = "Unit Player 2",
                Country = "Netherlands",
                City = "Eindhoven"
            },
            new Player
            {
                PlayerId = 3,
                Username = "UnitPLayer3",
                Password = "1234",
                Role = "player",
                PlayerLevel = 0,
                Experience = 0,
                SkillPoints = 5,
                Money = 100,
                Income = 1,
                Energy = 100,
                EnergyRegen = 5,
                RefillableEnergy = 100,
                LastTimeEnergyRefilled = DateTime.Now,
                MaxEnergy = 100,
                RealName = "Unit Player 3",
                Country = "Netherlands",
                City = "Eindhoven"
            }
        };

        private List<Hack> _hacks = new List<Hack>()
        {
            new Hack
            {
                HackId = 1,
                Name = "SQL-injection",
                Description = "Inject Malicious Code with the help of SQL",
                BaseDifficulty = 5,
                SkillCategoryId = 1,
                SkillDifficulty = 5,
                EnergyCost = 5,
                Reward = 10,
                RewardTypeId = 1,
                RewardName = "Money",
                SkillCategoryName = "Database",
                MinimalLevel = 1
            },
            new Hack
            {
                HackId = 2,
                Name = "DDOS",
                Description = "Overwelm a server with requests",
                BaseDifficulty = 10,
                SkillCategoryId = 2,
                SkillDifficulty = 5,
                EnergyCost = 10,
                Reward = 25,
                RewardTypeId = 1,
                RewardName = "Money",
                SkillCategoryName = "Servers",
                MinimalLevel = 2
            },
            new Hack
            {
                HackId = 3,
                Name = "Sneak Peak",
                Description = "Look over shoulder of Expert Hacker",
                BaseDifficulty = 0,
                SkillCategoryId = 3,
                SkillDifficulty = 0,
                EnergyCost = 3,
                Reward = 3,
                RewardTypeId = 3,
                RewardName = "Experience",
                SkillCategoryName = "Study",
                MinimalLevel = 0
            }
        };

        public List<Player> GetPlayers()
        {
            return _players;
        }

        public List<Player> GetPlayersWithoutGang()
        {
            throw new NotImplementedException();
            //return _players;
        }

        public Player GetPlayerWithId(int id)
        {
            throw new NotImplementedException();
        }

        public Player Login(string username, string password)
        {
            Player player = _players.First(e => e.Username == username && e.Password == password);
            return player;
        }

        public List<Hack> GetAvailableHacks(int minimalLevel)
        {
            IEnumerable<Hack> hacksEnumerable = _hacks.Where(e => e.MinimalLevel <= minimalLevel);
            List<Hack> hacks = new List<Hack>();
            hacks.AddRange(hacksEnumerable);
            return hacks;
        }

        public void UpdatePlayerLevels()
        {
            throw new NotImplementedException();
        }

        public bool PassedDifficultyCheck(int hackId, int playerId)
        {
            throw new NotImplementedException();
        }

        public void GivePlayerReward(int id, int playerId)
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetPlayerSkills(int playerId)
        {
            throw new NotImplementedException();
        }

        public Skill GetPlayerSkillWithId(int skillId, int playerId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerSkill(int skillId, int playerId)
        {
            throw new NotImplementedException();
        }

        public void LowerPlayerSkillPoints(int playerId)
        {
            throw new NotImplementedException();
        }

        public int UpdatePlayerEnergy(int playerId, int playerRefillableEnergy, int energy)
        {
            throw new NotImplementedException();
        }

        public void ConsumeEnergy(int hackId, int playerId)
        {
            throw new NotImplementedException();
        }

        public bool HasEnoughEnergy(int id, int playerId)
        {
            throw new NotImplementedException();
        }

        public List<Achievement> GetPlayerAchievements(int playerId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerHackStats(int id, int playerId, bool isSucces)
        {
            throw new NotImplementedException();
        }

        public void UpdateSinglePlayerLevel(int playerId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerData(Player player)
        {
            throw new NotImplementedException();
        }

        public List<Achievement> GetAllPlayersAchievements()
        {
            throw new NotImplementedException();
        }

        public List<PlayerLevel> GetAllPlayersLevels()
        {
            throw new NotImplementedException();
        }

        public bool IsUsernameTaken(string playerUsername)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(Player player)
        {
            throw new NotImplementedException();
        }

        public int CalculatePlayerLevel(int experience)
        {
            throw new NotImplementedException();
        }
    }
}
