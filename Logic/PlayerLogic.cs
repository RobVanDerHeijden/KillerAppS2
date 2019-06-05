using System;
using System.Collections.Generic;
using Data.Context.SQL;
using Data.Interfaces;
using Data.Repositorys;
using Model;

namespace Logic
{
    public class PlayerLogic
    {
        private static PlayerRepository PlayerRepo = new PlayerRepository();
        private readonly IPlayerContext _iplayerContext = new PlayerSqlContext();

        //private readonly IPlayerContext _iplayerContext;
        //public PlayerLogic(IPlayerContext iplayerContext)
        //{
        //    _iplayerContext = iplayerContext;
        //}

        public List<Player> GetPlayers()
        {
            return PlayerRepo.Players;
        }

        public List<Player> GetPlayersWithoutGang()
        {
            return PlayerRepo.GetPlayersWithoutGang();
        }

        public Player Login(string username, string password)
        {
            return PlayerRepo.Login(username, password);
        }

        public List<Hack> GetAvailableHacks(int id)
        {
            return PlayerRepo.GetAvailableHakcs(id);
        }

        public void UpdatePlayerLevels()
        {
            PlayerRepo.UpdatePlayerLevels();
        }

        public bool IsHackSuccessful(int hackId, int playerId)
        {
            if (PlayerRepo.IsHackSuccessful(hackId, playerId))
            {

            }
            return PlayerRepo.IsHackSuccessful(hackId, playerId);
        }

        public void GivePlayerReward(int id, int playerId)
        {
            PlayerRepo.GivePlayerReward(id, playerId);
        }

        public List<Skill> GetPlayerSkills(int playerId)
        {
            return PlayerRepo.GetPlayerSkills(playerId);
        }

        public bool UpgradeSkill(int skillId, int playerId)
        {
            if (_iplayerContext.GetPlayerWithId(playerId).SkillPoints > 0 &&
                _iplayerContext.GetPlayerSkillWithId(skillId, playerId).SkillPoints < _iplayerContext.GetPlayerSkillWithId(skillId, playerId).MaxSkillPoints)
            {
                return true;
            }

            return false;
        }
    }
}
