using System.Collections.Generic;
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
    }
}