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
        
        Player Login(string username, string password);
        List<Hack> GetAvailableHacks(int id);

        void UpdatePlayerLevels();
        bool PassedDifficultyCheck(int hackId, int playerId);
        void GivePlayerReward(int id, int playerId);
        List<Skill> GetPlayerSkills(int playerId);
        Skill GetPlayerSkillWithId(int skillId, int playerId);
    }
}