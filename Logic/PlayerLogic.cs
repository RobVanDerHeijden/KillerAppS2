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
    }
}
