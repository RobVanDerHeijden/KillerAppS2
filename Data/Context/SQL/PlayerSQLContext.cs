using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    using (SqlCommand cmd = new SqlCommand("SELECT  * FROM Player WHERE PlayerID NOT IN (SELECT PlayerId FROM GangMember)", conn))
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
    }
}