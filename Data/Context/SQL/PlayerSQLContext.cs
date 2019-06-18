using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using Data.Interfaces;
using Model;

namespace Data.Context.SQL
{
    public class PlayerSqlContext : IPlayerContext
    {
        // TODO: Remove all throws in the catches
        // TODO: Rewrite catch exeption message to be less technical and more user friendly
        private readonly DBConnection _dbConnection = new DBConnection();
        
        public List<Player> GetPlayers()
        {
            try
            {
                List<Player> playerList = new List<Player>();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    // TODO: onderzoek hoe EXISTS werkt en of het beter/efficienter is
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Player", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Player player = new Player
                                {
                                    PlayerId = (int)reader["PlayerID"],
                                    Username = (string)reader["username"],
                                    Password = (string)reader["password"],
                                    PlayerLevel = (int)reader["playerLevel"],
                                    Experience = (int)reader["experience"],
                                    SkillPoints = (int)reader["skillPoints"],
                                    Money = (decimal)reader["money"],
                                    Income = (decimal)reader["income"],
                                    Energy = (int)reader["energy"],
                                    MaxEnergy = (int)reader["maxEnergy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString()
                                };
                                player.ExperienceNeededForNextLevel = (player.PlayerLevel * 5) ^ 2;
                                playerList.Add(player);
                            }
                        }
                    }
                }
                return playerList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Player> GetPlayersWithoutGang()
        {
            try
            {
                List<Player> playerList = new List<Player>();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    // TODO: onderzoek hoe EXISTS werkt en of het beter/efficienter is
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Player WHERE PlayerID NOT IN (SELECT PlayerId FROM GangMember)", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Player player = new Player
                                {
                                    PlayerId = (int)reader["PlayerID"],
                                    Username = (string)reader["username"],
                                    Password = (string)reader["password"],
                                    PlayerLevel = (int)reader["playerLevel"],
                                    Experience = (int)reader["experience"],
                                    SkillPoints = (int)reader["skillPoints"],
                                    Money = (decimal)reader["money"],
                                    Income = (decimal)reader["income"],
                                    Energy = (int)reader["energy"],
                                    MaxEnergy = (int)reader["maxEnergy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString()
                                };
                                player.ExperienceNeededForNextLevel = (player.PlayerLevel * 5) ^ 2;
                                playerList.Add(player);
                            }
                        }
                    }
                }
                return playerList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        

        // Checks if login credentials are valid -> Returns PlayerModel || Parameters username, password
        public Player Login(string username, string password)
        {
            try
            {
                Player player = null;
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmdPlayer = 
                        new SqlCommand("SELECT P.*, R.name as RoleName " +
                                       "FROM [dbo].[Player] P " +
                                       "INNER JOIN Role R ON R.RoleID = P.RoleID " +
                                       "WHERE P.username = @Username AND P.password = @Password", conn))
                    {
                        cmdPlayer.Parameters.AddWithValue("@Username", username);
                        cmdPlayer.Parameters.AddWithValue("@Password", password);
                        using (SqlDataReader reader = cmdPlayer.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                player = new Player
                                {
                                    PlayerId = (int)reader["PlayerID"],
                                    Username = (string)reader["username"],
                                    Password = (string)reader["password"],
                                    PlayerLevel = (int)reader["playerLevel"],
                                    Experience = (int)reader["experience"],
                                    SkillPoints = (int)reader["skillPoints"],
                                    Money = (decimal)reader["money"],
                                    Income = (decimal)reader["income"],
                                    Energy = (int)reader["energy"],
                                    MaxEnergy = (int)reader["maxEnergy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString(),
                                    Role = reader["RoleName"].ToString()
                                };
                                player.ExperienceNeededForNextLevel = (player.PlayerLevel * 5) ^ 2;
                            }
                        }
                    }
                }
                return player;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Returns List of Hacks available for player based on his level || Parameter id = playerLevel
        public List<Hack> GetAvailableHacks(int minimalLevel)
        {
            try
            {
                List<Hack> hacks = new List<Hack>();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = 
                        new SqlCommand("SELECT H.*, RT.name AS RewardName, SC.name AS SkillCategoryName " +
                                       "FROM Hack H " +
                                       "INNER JOIN RewardType RT ON RT.RewardTypeID = H.RewardTypeID " +
                                       "INNER JOIN SkillCategory SC ON SC.SkillCategoryID = H.SkillCategoryID " +
                                       "WHERE minimalLevel <= @id;", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", minimalLevel);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hack hack = new Hack
                                {
                                    HackId = (int) reader["HackID"],
                                    Name = (string) reader["name"],
                                    Description = (string) reader["description"],
                                    BaseDifficulty = (int) reader["baseDifficulty"],
                                    SkillCategoryId = (int) reader["SkillCategoryID"],
                                    SkillDifficulty = (int) reader["skillDifficulty"],
                                    EnergyCost = (int)reader["energyCost"],
                                    Reward = (int)reader["reward"],
                                    RewardTypeId = (int)reader["RewardTypeID"],
                                    RewardName = (string)reader["RewardName"],
                                    SkillCategoryName = (string)reader["SkillCategoryName"]
                                };
                                hacks.Add(hack);
                            }
                        }
                    }
                }
                return hacks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // STORED PROCEDURE: dbo.calcLevel
        // Recalculates players level with Query: UPDATE Player SET playerLevel = FLOOR(SQRT(experience)) * 0.2
        public void UpdatePlayerLevels()
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "dbo.calcLevel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["experience"]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PassedDifficultyCheck(int hackId, int playerId)
        {
            bool isHackSucces = false;
            Hack selectedHack = null;
            double totalSkillPointsInCategory = 0;
            double maxSkillPointsInCategory = 0;
            Random rnd = new Random();
            int succesChance = rnd.Next(1, 101);
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT H.*, RT.name AS RewardName, SC.name AS SkillCategoryName " +
                                                           "FROM Hack H " +
                                                           "INNER JOIN RewardType RT ON RT.RewardTypeID = H.RewardTypeID " +
                                                           "INNER JOIN SkillCategory SC ON SC.SkillCategoryID = H.SkillCategoryID " +
                                                           "WHERE HackID <= @HackID;", conn))
                    {
                        cmd.Parameters.AddWithValue("@HackID", hackId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                selectedHack = new Hack
                                {
                                    HackId = (int)reader["HackID"],
                                    BaseDifficulty = (int)reader["baseDifficulty"],
                                    SkillCategoryId = (int)reader["SkillCategoryID"],
                                    SkillDifficulty = (int)reader["skillDifficulty"],
                                };
                            }
                        }
                    }

                    if (selectedHack != null)
                    {
                        using (SqlCommand cmdPointsSkills = new SqlCommand(
                        "SELECT S.name, PS.skillPoints AS PlayerSkillPoints, PS.maxSkillPoints " +
                        "FROM Skill S " +
                        "INNER JOIN PlayerSkill PS ON PS.SkillID = S.SkillID " +
                        "WHERE S.SkillCategoryID = @SkillCategoryID " +
                        "AND PS.PlayerID = @playerID", conn))
                        {
                            cmdPointsSkills.Parameters.AddWithValue("@SkillCategoryID", selectedHack.SkillCategoryId);
                            cmdPointsSkills.Parameters.AddWithValue("@playerID", playerId);
                            using (SqlDataReader reader = cmdPointsSkills.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    totalSkillPointsInCategory += (int) reader["PlayerSkillPoints"];
                                    maxSkillPointsInCategory += (int) reader["maxSkillPoints"];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            double categoryCompletionRatio = totalSkillPointsInCategory / maxSkillPointsInCategory;
            if (selectedHack != null && (selectedHack.SkillDifficulty * categoryCompletionRatio) + selectedHack.BaseDifficulty <= succesChance)
            {
                isHackSucces = true;
            }
            return isHackSucces;
        }

        public void GivePlayerReward(int hackId, int playerId)
        {
            int rewardType = 0;
            int rewardAmount = 0;
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    // Maak hier Inner join van en zet letterlijke waarde in db, om zo de switch case weg te kunnen halen
                    using (SqlCommand cmd = new SqlCommand("SELECT RewardTypeID, reward FROM Hack WHERE HackID = @HackID",conn))
                    {
                        cmd.Parameters.AddWithValue("@HackID", hackId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rewardType = (int)reader["RewardTypeID"];
                                rewardAmount = (int)reader["reward"];
                            }
                        }
                    }
                }

                switch (rewardType)
                {
                    case 1: // Money
                        using (SqlConnection conn = _dbConnection.GetConnString())
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("UPDATE Player SET money = money + @rewardAmount WHERE PlayerID = @PlayerID", conn))
                            {
                                cmd.Parameters.AddWithValue("@rewardAmount", rewardAmount);
                                cmd.Parameters.AddWithValue("@PlayerID", playerId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        break;
                    case 2: // Skill Points
                        using (SqlConnection conn = _dbConnection.GetConnString())
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("UPDATE Player SET skillPoints = skillPoints + @rewardAmount WHERE PlayerID = @PlayerID", conn))
                            {
                                cmd.Parameters.AddWithValue("@rewardAmount", rewardAmount);
                                cmd.Parameters.AddWithValue("@PlayerID", playerId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        break;
                    case 3: // Experience
                        using (SqlConnection conn = _dbConnection.GetConnString())
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("UPDATE Player SET experience = experience + @rewardAmount WHERE PlayerID = @PlayerID", conn))
                            {
                                cmd.Parameters.AddWithValue("@rewardAmount", rewardAmount);
                                cmd.Parameters.AddWithValue("@PlayerID", playerId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Skill> GetPlayerSkills(int playerId)
        {
            List<Skill> playerSkills = new List<Skill>();
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT S.SkillID, S.name AS SkillName, S.description, PS.skillPoints, PS.maxSkillPoints, SC.name AS SkillCategoryName " +
                                                           "FROM Skill S " +
                                                           "INNER JOIN PlayerSkill PS ON PS.SkillID = S.SkillID " +
                                                           "INNER JOIN SkillCategory SC ON SC.SkillCategoryID = S.SkillCategoryID " +
                                                           "WHERE PS.PlayerId = @playerId", conn))
                    {
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Skill skill = new Skill
                                {
                                    SkillId = (int) reader["SkillID"],
                                    Name = (string) reader["SkillName"],
                                    Description = (string) reader["description"],
                                    SkillPoints = (int) reader["skillPoints"],
                                    MaxSkillPoints = (int) reader["maxSkillPoints"],
                                    SkillCategoryName = (string) reader["SkillCategoryName"]
                                };
                                playerSkills.Add(skill);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return playerSkills;
        }
        public Player GetPlayerWithId(int playerId)
        {
            try
            {
                Player player = new Player();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT P.*, R.name as RoleName " +
                                       "FROM Player P " +
                                       "INNER JOIN Role R ON R.RoleID = P.RoleID " +
                                       "WHERE PlayerID = @PlayerID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", playerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                player = new Player
                                {
                                    PlayerId = (int) reader["PlayerID"],
                                    Username = (string) reader["username"],
                                    Password = (string) reader["password"],
                                    PlayerLevel = (int) reader["playerLevel"],
                                    Experience = (int) reader["experience"],
                                    SkillPoints = (int) reader["skillPoints"],
                                    Money = (decimal) reader["money"],
                                    Income = (decimal) reader["income"],
                                    Energy = (int) reader["energy"],
                                    EnergyRegen = (int)reader["energyRegen"],
                                    RefillableEnergy = (int)reader["refillableEnergy"],
                                    LastTimeEnergyRefilled = (DateTime) reader["lastTimeEnergyRefilled"],
                                    MaxEnergy = (int)reader["maxEnergy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString(),
                                    Role = reader["RoleName"].ToString()
                                };
                                int nextLevel = player.PlayerLevel+1;
                                player.ExperienceNeededForNextLevel = (int) Math.Pow((nextLevel * 5), 2);
                            }
                        }
                    }

                    // Set RefillableEnergy to correct value based on last check
                    DateTime startTime = player.LastTimeEnergyRefilled;
                    DateTime endTime = DateTime.Now;
                    double timeDiffInMinutes = endTime.Subtract(startTime).TotalMinutes;

                    if (timeDiffInMinutes > 5)
                    {
                        int newRefillableEnergy = player.RefillableEnergy + (int)Math.Floor((timeDiffInMinutes / 5)) * player.EnergyRegen;
                        if (newRefillableEnergy > 100)
                        {
                            newRefillableEnergy = 100;
                        }
                        DateTime xMinsLater = startTime.AddMinutes((int)Math.Floor((timeDiffInMinutes / 5)) * 5);
                        using (SqlCommand cmd = new SqlCommand("UPDATE Player Set refillableEnergy = @playerRefillableEnergy, lastTimeEnergyRefilled = @updatedLastTime WHERE PlayerID = @playerId", conn))
                        {
                            cmd.Parameters.AddWithValue("@playerId", playerId);
                            cmd.Parameters.AddWithValue("@playerRefillableEnergy", newRefillableEnergy);
                            cmd.Parameters.AddWithValue("@updatedLastTime", xMinsLater);

                            cmd.ExecuteNonQuery();
                        }
                        player.RefillableEnergy = newRefillableEnergy;
                        player.LastTimeEnergyRefilled = xMinsLater;
                    }
                }
                return player;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Skill GetPlayerSkillWithId(int skillId, int playerId)
        {
            try
            {
                Skill skill = new Skill();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT S.SkillID, S.name AS SkillName, S.description, PS.skillPoints, PS.maxSkillPoints, SC.name AS SkillCategoryName " +
                                                           "FROM Skill S " +
                                                           "INNER JOIN PlayerSkill PS ON PS.SkillID = S.SkillID " +
                                                           "INNER JOIN SkillCategory SC ON SC.SkillCategoryID = S.SkillCategoryID " +
                                                           "WHERE PS.PlayerId = @playerId " +
                                                           "AND S.SkillID = @SkillID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SkillID", skillId);
                        cmd.Parameters.AddWithValue("@PlayerID", playerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                skill = new Skill()
                                {
                                    SkillId = (int) reader["SkillID"],
                                    Name = (string) reader["SkillName"],
                                    Description = (string) reader["description"],
                                    SkillPoints = (int) reader["skillPoints"],
                                    MaxSkillPoints = (int) reader["maxSkillPoints"],
                                    SkillCategoryName = (string) reader["SkillCategoryName"]
                                };
                            }
                        }
                    }
                }
                return skill;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdatePlayerSkill(int skillId, int playerId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE PlayerSkill SET skillPoints = skillPoints + 1 WHERE PlayerID = @PlayerID AND SkillID = @SkillID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SkillID", skillId);
                        cmd.Parameters.AddWithValue("@PlayerID", playerId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void LowerPlayerSkillPoints(int playerId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();

                    using (SqlCommand cmd = 
                        new SqlCommand("UPDATE Player " +
                                       "SET skillPoints = skillPoints - 1 " +
                                       "WHERE PlayerID = @PlayerID", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", playerId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // playerid, refillableEnergy, new Energy
        public int UpdatePlayerEnergy(int playerId, int playerRefillableEnergy, int energy)
        {
            int excessEnergy = 0;
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Player Set refillableEnergy = @playerRefillableEnergy, energy = @energy, lastTimeEnergyRefilled = getdate() WHERE PlayerID = @playerId", conn))
                    {
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        cmd.Parameters.AddWithValue("@playerRefillableEnergy", playerRefillableEnergy);
                        cmd.Parameters.AddWithValue("@energy", energy);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return excessEnergy;
        }

        public void ConsumeEnergy(int hackId, int playerId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    int energyToConsume;

                    using (SqlCommand cmdEnergyCost = new SqlCommand("SELECT energyCost FROM Hack WHERE hackId = @hackId", conn))
                    {
                        cmdEnergyCost.Parameters.AddWithValue("@hackId", hackId);
                        energyToConsume = (int)cmdEnergyCost.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE Player SET energy = energy - @energyToConsume WHERE PlayerID = @playerId", conn))
                    {
                        cmd.Parameters.AddWithValue("@energyToConsume", energyToConsume);
                        cmd.Parameters.AddWithValue("@playerId", playerId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool HasEnoughEnergy(int id, int playerId)
        {
            bool hasEnoughEnergy;
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    int energyToConsume;
                    int playerEnergy;

                    using (SqlCommand cmdEnergyCost = new SqlCommand("SELECT energyCost FROM Hack WHERE hackId = @hackId", conn))
                    {
                        cmdEnergyCost.Parameters.AddWithValue("@hackId", id);
                        energyToConsume = (int)cmdEnergyCost.ExecuteScalar();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT energy FROM Player WHERE PlayerID = @playerId", conn))
                    {
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        playerEnergy = (int)cmd.ExecuteScalar();
                    }

                    if (energyToConsume > playerEnergy)
                    {
                        hasEnoughEnergy = false;
                    }
                    else
                    {
                        hasEnoughEnergy = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return hasEnoughEnergy;
        }

        public List<Achievement> GetPlayerAchievements(int playerId)
        {
            List<Achievement> playerAchievements = new List<Achievement>();
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT A.AchievementID, A.name AS AchievementName, A.description, PA.dateAchieved " +
                                                           "FROM Achievement A " +
                                                           "INNER JOIN PlayerAchievement PA ON PA.AchievementID = A.AchievementID " +
                                                           "WHERE PA.PlayerId = @playerId " +
                                                           "ORDER BY AchievementID", conn))
                    {
                        cmd.Parameters.AddWithValue("@playerId", playerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Achievement achievement = new Achievement
                                {
                                    AchievementId = (int) reader["AchievementID"],
                                    Name = (string)reader["AchievementName"],
                                    Description = (string)reader["description"],
                                    DateAchieved = (DateTime) reader["dateAchieved"]
                                };
                                playerAchievements.Add(achievement);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return playerAchievements;
        }

        public void UpdatePlayerHackStats(int id, int playerId, bool isSucces)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    if (isSucces)
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE PlayerHack SET timesSucces = timesSucces + 1 WHERE PlayerID = @PlayerID AND HackID = @HackID", conn))
                        {
                            cmd.Parameters.AddWithValue("@HackID", id);
                            cmd.Parameters.AddWithValue("@PlayerID", playerId);

                            cmd.ExecuteNonQuery();
                        }
                    } else if (!isSucces)
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE PlayerHack SET timesFailed = timesFailed + 1 WHERE PlayerID = @PlayerID AND HackID = @HackID", conn))
                        {
                            cmd.Parameters.AddWithValue("@HackID", id);
                            cmd.Parameters.AddWithValue("@PlayerID", playerId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // STORED PROCEDURE: dbo.calcLevelOnePlayer
        public void UpdateSinglePlayerLevel(int playerId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(
                        "dbo.calcLevelOnePlayer", conn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", playerId);
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["experience"]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdatePlayerData(Player player)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Player " +
                                                           "SET username = @Username, password = @Password, realName = @RealName, country = @Country, city = @City " +
                                                           "WHERE PlayerID = @playerId", conn))
                    {
                        cmd.Parameters.AddWithValue("@playerId", player.PlayerId);
                        cmd.Parameters.AddWithValue("@Username", player.Username);
                        cmd.Parameters.AddWithValue("@Password", player.Password);
                        cmd.Parameters.AddWithValue("@RealName", player.RealName);
                        cmd.Parameters.AddWithValue("@Country", player.Country);
                        cmd.Parameters.AddWithValue("@City", player.City);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Achievement> GetAllPlayersAchievements()
        {
            List<Achievement> playerAchievements = new List<Achievement>();
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT A.name AS AchievementName, P.username AS PlayerName,  PA.dateAchieved " +
                                                           "FROM Player P " +
                                                           "LEFT JOIN PlayerAchievement PA ON PA.PlayerID = P.PlayerID " +
                                                           "RIGHT JOIN Achievement A ON A.AchievementID = PA.AchievementID " +
                                                           "ORDER BY A.AchievementID ASC, PA.dateAchieved ASC; ", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Achievement achievement = new Achievement
                                {
                                    AchievementName = (string)reader["AchievementName"],
                                    PlayerName = reader["PlayerName"]?.ToString(),
                                    DateAchieved = reader["dateAchieved"] as DateTime?
                                };
                                playerAchievements.Add(achievement);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return playerAchievements;
        }

        public List<PlayerLevel> GetAllPlayersLevels()
        {
            List<PlayerLevel> playersLevels = new List<PlayerLevel>();
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT playerLevel, COUNT(playerLevel) AS AmountPlayers " +
                                                           "FROM Player " +
                                                           "GROUP BY playerLevel", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlayerLevel playerLevel = new PlayerLevel
                                {
                                    Level = (int) reader["playerLevel"],
                                    AmountPlayers = (int) reader["AmountPlayers"]
                                };
                                playersLevels.Add(playerLevel);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return playersLevels;
        }

        public bool IsUsernameTaken(string playerUsername)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * " +
                                                           "FROM Player " +
                                                           "WHERE username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", playerUsername);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return false;
        }

        public void RegisterUser(Player player)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Player (username, password, realName, country, city) " +
                                                           "VALUES(@Username, @Password, @RealName, @Country, @City); ", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", player.Username);
                        cmd.Parameters.AddWithValue("@Password", player.Password);
                        cmd.Parameters.AddWithValue("@RealName", player.RealName);
                        cmd.Parameters.AddWithValue("@Country", player.Country);
                        cmd.Parameters.AddWithValue("@City", player.City);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
 