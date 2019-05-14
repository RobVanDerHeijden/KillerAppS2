using System.Collections.Generic;
using Data.Context;
using Data.Interfaces;
using Model;

namespace Data.Repositorys
{
    public class PlayerRepository
    {
        private readonly IPlayerContext _iplayerContext;

        public PlayerRepository()
        {
            _iplayerContext = new PlayerMemoryContext();
        }
        public List<Player> Players => _iplayerContext.GetPlayers();
    }
}