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
            if (_iplayerContext.GetPlayerWithId(playerId).SkillPoints > 0 &&
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
            return _iplayerContext.GetPlayerWithId(playerId);
        }
    }
}
