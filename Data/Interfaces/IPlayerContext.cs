using System.Collections.Generic;
using Model;

namespace Data.Interfaces
{
    public interface IPlayerContext
    {
        int GetLevel(int playerId);
        List<Player> GetPlayers();
        List<Player> GetPlayersWithoutGang();
        Player GetPlayerWithId(int id);
        void UpdatePlayer(Player player);
        void RemovePlayer(int id);
        void CreatePlayer(Player player);

    }
}