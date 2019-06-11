using Data.Context;
using Data.Interfaces;
using System.Collections.Generic;
using Data.Context.SQL;
using Model;

namespace Data.Repositorys
{
    public class GangRepository
    {
        private readonly IGangContext _igangContext;
        //private readonly IPlayerContext _iplayerContext;

        public GangRepository()
        {
            _igangContext = new GangSqlContext();
            //_iplayerContext = new PlayerMemoryContext();
        }

        public List<Gang> Gangs => _igangContext.GetGangs();
        
        public IEnumerable<Player> GetPlayerInGang(int gangId)
        {
            return _igangContext.GetPlayersInGang(gangId);
        }

        public void AddPlayerToGang(int playerId, int gangId)
        {
            _igangContext.AddPlayerToGang(playerId, gangId);
        }
        public void RemovePlayerFromGang(int playerId, int gangId)
        {
            _igangContext.RemovePlayerFromGang(playerId, gangId);
        }
        public void RemovePlayerFromGang(int playerId)
        {
            _igangContext.RemovePlayerFromGang(playerId);
        }
        
        public Gang GetGangWithId(int id)
        {
            return _igangContext.GetGangWithId(id);
        }

        public void UpdateGang(Gang gang)
        {
            _igangContext.UpdateGang(gang);
        }

        public void RemoveGang(int id)
        {
            _igangContext.RemoveGang(id);
        }

        public void CreateGang(Gang gang)
        {
            _igangContext.CreateGang(gang);
        }
    }
}