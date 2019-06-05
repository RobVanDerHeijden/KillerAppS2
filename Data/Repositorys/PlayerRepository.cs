﻿using System.Collections.Generic;
using System.Dynamic;
using Data.Context;
using Data.Context.SQL;
using Data.Interfaces;
using Model;

namespace Data.Repositorys
{
    public class PlayerRepository
    {
        private readonly IPlayerContext _iplayerContext;

        public PlayerRepository()
        {
            _iplayerContext = new PlayerSqlContext();
        }
        public List<Player> Players => _iplayerContext.GetPlayers();

        public List<Player> GetPlayersWithoutGang()
        {
            return _iplayerContext.GetPlayersWithoutGang();
        }

        public Player Login(string username, string password)
        {
            return _iplayerContext.Login(username, password);
        }

        public List<Hack> GetAvailableHakcs(int id)
        {
            return _iplayerContext.GetAvailableHacks(id);
        }

        public void UpdatePlayerLevels()
        {
            _iplayerContext.UpdatePlayerLevels();
        }

        public bool IsHackSuccessful(int hackId, int playerId)
        {
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
    }
}