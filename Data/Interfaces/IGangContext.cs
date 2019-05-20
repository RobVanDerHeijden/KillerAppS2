using System.Collections.Generic;
using Model;

namespace Data.Interfaces
{
    public interface IGangContext
    {
        List<Gang> GetGangs();
        IEnumerable<Player> GetPlayersInGang(int id);
        void AddPlayerToGang(int playerId, int gangId);
        void RemovePlayerFromGang(int playerId, int gangId);
        void RemovePlayerFromGang(int playerId);
        
        Gang GetGangWithId(int id);
        void UpdateGang(Gang gang);
        void RemoveGang(int id);
        void CreateGang(Gang gang);
    }
}