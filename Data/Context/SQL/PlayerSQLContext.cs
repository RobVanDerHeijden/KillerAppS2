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

        public Player Login(string username, string password)
        {
            try
            {
                Player player = null;
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    // select player where playerid = playerid & password = password
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Player] WHERE username = @Username AND password = @Password", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
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
                                    City = reader["city"]?.ToString()
                                    //Role = 
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



            //using (DB_Entities db = new DB_Entities())
            //{
            //    var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
            //    if (obj != null)
            //    {
            //        Session["UserID"] = obj.UserId.ToString();
            //        Session["UserName"] = obj.UserName.ToString();
            //        return RedirectToAction("UserDashBoard");
            //    }
            //}
        }


    }
}