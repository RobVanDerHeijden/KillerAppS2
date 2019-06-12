using System;
using System.Collections.Generic;
using Data.Context.SQL;
using Data.Interfaces;
using Data.Repositorys;
using Microsoft.AspNetCore.Http;
using Model;

namespace Logic
{
    public class PlayerLogic
    {
        //private static PlayerRepository PlayerRepo = new PlayerRepository();
        private readonly IPlayerContext _iplayerContext = new PlayerSqlContext();
        
        //private readonly IPlayerContext _iplayerContext;
        //public PlayerLogic(IPlayerContext iplayerContext)
        //{
        //    _iplayerContext = iplayerContext;
        //}

        public List<Player> GetPlayers()
        {
            return _iplayerContext.GetPlayers();
        }

        public List<Player> GetPlayersWithoutGang()
        {
            return _iplayerContext.GetPlayersWithoutGang();
        }

        public Player Login(string username, string password)
        {
            return _iplayerContext.Login(username, password);
        }

        public List<Hack> GetAvailableHacks(int id)
        {
            return _iplayerContext.GetAvailableHacks(id);
        }

        public void UpdatePlayerLevels()
        {
            _iplayerContext.UpdatePlayerLevels();
        }

        public bool IsHackSuccessful(int hackId, int playerId)
        {
            if (_iplayerContext.PassedDifficultyCheck(hackId, playerId))
            {
                _iplayerContext.ConsumeEnergy(hackId, playerId);
            }
            return _iplayerContext.PassedDifficultyCheck(hackId, playerId);
        }

        public void GivePlayerReward(int id, int playerId)
        {
            _iplayerContext.GivePlayerReward(id, playerId);
        }

        public List<Skill> GetPlayerSkills(int playerId)
        {
            return _iplayerContext.GetPlayerSkills(playerId);
        }

        // Return type is bool so I can set a appropriate message in Tempdata
        public bool UpgradeSkill(int skillId, int playerId)
        {
            Player player = _iplayerContext.GetPlayerWithId(playerId);
            if (player.SkillPoints > 0 &&
                _iplayerContext.GetPlayerSkillWithId(skillId, playerId).SkillPoints < _iplayerContext.GetPlayerSkillWithId(skillId, playerId).MaxSkillPoints)
            {
                _iplayerContext.UpdatePlayerSkill(skillId, playerId);
                _iplayerContext.LowerPlayerSkillPoints(playerId);
                return true;
            }
            return false;
        }

        public Player GetPlayerWithId(int playerId)
        {
            Player player = _iplayerContext.GetPlayerWithId(playerId);

            return player;
        }

        public bool UpdatePlayerEnergy(int playerId)
        {
            Player player = _iplayerContext.GetPlayerWithId(playerId);

            // Difference between now and LastTimeEnergyRefilled
            DateTime startTime = player.LastTimeEnergyRefilled;
            DateTime endTime = DateTime.Now;
            double timeDiffInMinutes = endTime.Subtract(startTime).TotalMinutes;

            // For every 5 mins in difference, add energyRegen(default = 5)
            player.RefillableEnergy += (int) Math.Floor((timeDiffInMinutes / 5)) * player.EnergyRegen;

            // Handeling of exess Energy and updating the new Energy
            if (player.RefillableEnergy > 0 && 
                player.Energy < player.MaxEnergy)
            {
                int excessEnergy;
                if (player.Energy + player.RefillableEnergy > player.MaxEnergy)
                {
                    excessEnergy = player.Energy + player.RefillableEnergy - player.MaxEnergy;
                    _iplayerContext.UpdatePlayerEnergy(player.PlayerId, excessEnergy, player.MaxEnergy);
                }
                else
                {
                    excessEnergy = 0;
                    _iplayerContext.UpdatePlayerEnergy(player.PlayerId, excessEnergy, player.Energy + player.RefillableEnergy);
                }
                return true;
            }
            return false;
        }

        public bool HasEnoughEnergy(int id, int playerId)
        {
            return _iplayerContext.HasEnoughEnergy(id, playerId);
        }

        public List<Achievement> GetPlayerAchievements(int playerId)
        {
            return _iplayerContext.GetPlayerAchievements(playerId);
        }

        public void UpdatePlayerHackStats(int id, int playerId, bool isSucces)
        {
            _iplayerContext.UpdatePlayerHackStats(id, playerId, isSucces);
        }

        public void UpdateSinglePlayerLevel(int playerId)
        {
            Player player = _iplayerContext.GetPlayerWithId(playerId);
            _iplayerContext.UpdateSinglePlayerLevel(playerId);
        }

        public void UpdatePlayerData(Player player)
        {
            _iplayerContext.UpdatePlayerData(player);
        }

        public List<Achievement> GetAllPlayersAchievements()
        {
            return _iplayerContext.GetAllPlayersAchievements();
        }

        public List<PlayerLevel> GetAllPlayersLevels()
        {
            return _iplayerContext.GetAllPlayersLevels();
        }
    }
}
