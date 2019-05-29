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
        private readonly DBConnection _dbConnection = new DBConnection();

        public int GetLevel(int playerId)
        {
            throw new System.NotImplementedException();
        }

        public List<Player> GetPlayers()
        {
            try
            {
                List<Player> playerList = new List<Player>();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    // TODO: onderzoek hoe EXISTS werkt en of het beter/efficienter is
                    using (SqlCommand cmd = new SqlCommand("SELECT  * FROM Player", conn))
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
                                    Experience = (decimal)reader["experience"],
                                    SkillPoints = (int)reader["skillPoints"],
                                    Money = (decimal)reader["money"],
                                    Income = (decimal)reader["income"],
                                    Energy = (int)reader["energy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString()
                                };
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
                                    Experience = (decimal)reader["experience"],
                                    SkillPoints = (int)reader["skillPoints"],
                                    Money = (decimal)reader["money"],
                                    Income = (decimal)reader["income"],
                                    Energy = (int)reader["energy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString()
                                };
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

        public Player GetPlayerWithId(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePlayer(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void RemovePlayer(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CreatePlayer(Player player)
        {
            throw new System.NotImplementedException();
        }

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
                                    Experience = (decimal)reader["experience"],
                                    SkillPoints = (int)reader["skillPoints"],
                                    Money = (decimal)reader["money"],
                                    Income = (decimal)reader["income"],
                                    Energy = (int)reader["energy"],
                                    RealName = reader["realName"]?.ToString(),
                                    Country = reader["country"]?.ToString(),
                                    City = reader["city"]?.ToString(),
                                    Role = reader["RoleName"].ToString()
                                };
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

        public List<Hack> GetAvailableHacks(int id)
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
                        cmd.Parameters.AddWithValue("@id", id);
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
                                Console.WriteLine(
                                    reader["experience"]);
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
    }
}