using System;
using System.Collections.Generic;
using Data.Repositorys;
using Model;

namespace Logic
{
    public class PlayerLogic
    {
        private static PlayerRepository PlayerRepo = new PlayerRepository();

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
            return PlayerRepo.IsHackSuccessful(hackId, playerId);
        }
    }
}
