using System.Collections.Generic;
using Model;

namespace Data.Interfaces
{
    public interface IPlayerContext
    {
        List<Player> GetPlayers();
        List<Player> GetPlayersWithoutGang();
        Player GetPlayerWithId(int id);
        Player Login(string username, string password);
        List<Hack> GetAvailableHacks(int id);
        bool PassedDifficultyCheck(int hackId, int playerId);
        void GivePlayerReward(int id, int playerId);
        List<Skill> GetPlayerSkills(int playerId);
        Skill GetPlayerSkillWithId(int skillId, int playerId);
        void UpdatePlayerSkill(int skillId, int playerId);
        void LowerPlayerSkillPoints(int playerId);
        int UpdatePlayerEnergy(int playerId, int playerRefillableEnergy, int energy);
        void ConsumeEnergy(int hackId, int playerId);
        bool HasEnoughEnergy(int id, int playerId);
        List<Achievement> GetPlayerAchievements(int playerId);
        void UpdatePlayerHackStats(int id, int playerId, bool isSucces);
        void UpdateSinglePlayerLevel(int playerId);
        void UpdatePlayerData(Player player);
        List<Achievement> GetAllPlayersAchievements();
        List<PlayerLevel> GetAllPlayersLevels();
        bool IsUsernameTaken(string playerUsername);
        void RegisterUser(Player player);
    }
}