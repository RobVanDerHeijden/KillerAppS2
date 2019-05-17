using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context.SQL
{
    class GangSqlContext : IGangContext
    {
        private DBConnection dbConnection = new DBConnection();

        public List<Gang> GetGangs()
        {
            List<Gang> gangs = new List<Gang>();
            try
            {
                using (SqlConnection conn = dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand gangCmd = new SqlCommand("SELECT * FROM [dbo].[Gang];", conn))
                    {
                        using (SqlDataReader gangReader = gangCmd.ExecuteReader())
                        {
                            while (gangReader.Read())
                            {
                                Gang gang = new Gang();
                                gang.GangId = (int) gangReader["GangID"];
                                gang.Name = (string) gangReader["name"];
                                gang.Tag = (string) gangReader["tag"];
                                gang.Description = gangReader["description"]?.ToString();
                                gang.Money = (decimal) gangReader["money"];
                                gang.Income = (decimal) gangReader["income"];
                                gang.DateFounded = (DateTime) gangReader["dateFounded"];
                                gangs.Add(gang);
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

            foreach (Gang currentgang in gangs)
            {
                currentgang.Players = GetPlayersInGang(currentgang.GangId);
            }
            return gangs;
        }

        public IEnumerable<Player> GetPlayersInGang(int gangId)
        {
            try
            {
                List<Player> playerList = new List<Player>();
                using (SqlConnection conn = dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand gangMemberCmd = 
                            //new SqlCommand("SELECT * FROM[dbo].[Player]", con))
                        new SqlCommand("SELECT P.* " +
                                       "FROM [dbo].[Player] P " +
                                       "INNER JOIN [dbo].[GangMember] GM ON GM.PlayerID = P.PlayerID " +
                                       "WHERE GangID = @GangID", conn))
                    {
                        gangMemberCmd.Parameters.AddWithValue("@GangID", gangId);
                        using (SqlDataReader gangMemberReader = gangMemberCmd.ExecuteReader())
                        {
                            while (gangMemberReader.Read())
                            {
                                Player player = new Player
                                {
                                    PlayerId = (int)gangMemberReader["PlayerID"],
                                    Username = (string)gangMemberReader["username"],
                                    Password = (string)gangMemberReader["password"],
                                    PlayerLevel = (int)gangMemberReader["playerLevel"],
                                    Experience = (decimal)gangMemberReader["experience"],
                                    SkillPoints = (int)gangMemberReader["skillPoints"],
                                    Money = (decimal)gangMemberReader["money"],
                                    Income = (decimal)gangMemberReader["income"],
                                    Energy = (int)gangMemberReader["energy"],
                                    RealName = gangMemberReader["realName"]?.ToString(),
                                    Country = gangMemberReader["country"]?.ToString(),
                                    City = gangMemberReader["city"]?.ToString()
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

        public void AddPlayerToGang(int playerId, int gangId)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayerFromGang(int playerId, int gangId)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayersWithoutGang()
        {
            throw new NotImplementedException();
        }

        public Gang GetGangWithId(int gangId)
        {
            try
            {
                Gang gang = new Gang();
                using (SqlConnection conn = dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Gang] WHERE GangId = @GangId", conn))
                    {
                        cmd.Parameters.AddWithValue("@GangId", gangId);
                        using (SqlDataReader gangReader = cmd.ExecuteReader())
                        {
                            while (gangReader.Read())
                            {
                                gang.GangId = (int)gangReader["GangID"];
                                gang.Name = (string)gangReader["name"];
                                gang.Tag = (string)gangReader["tag"];
                                gang.Description = gangReader["description"]?.ToString();
                                gang.Money = (decimal)gangReader["money"];
                                gang.Income = (decimal)gangReader["income"];
                                gang.DateFounded = (DateTime)gangReader["dateFounded"];
                            }
                        }
                    }
                }

                gang.Players = GetPlayersInGang(gangId);
                return gang;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateGang(Gang gang)
        {
            try
            {
                using (SqlConnection conn = dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Gang] SET name = @name, tag = @tag, description = @description WHERE GangId = @GangId", conn))
                    {
                        cmd.Parameters.AddWithValue("@GangId", gang.GangId);
                        cmd.Parameters.AddWithValue("@name", gang.Name);
                        cmd.Parameters.AddWithValue("@tag", gang.Tag);
                        cmd.Parameters.AddWithValue("@description", gang.Description);
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

        public void RemoveGang(int id)
        {
            throw new NotImplementedException();
        }

        // TODO: Add current player to gang as leader when gang is created
        public void CreateGang(Gang gang)
        {
            try
            {
                using (SqlConnection conn = dbConnection.GetConnString())
                {
                    conn.Open();
                    //INSERT INTO Customers(CustomerName, ContactName, Address, City, PostalCode, Country)
                    //VALUES('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway');
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Gang] (name, tag, description, money, income, dateFounded) " +
                                                           "VALUES(@name, @tag, @description, @money, @income, @dateFounded)", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", gang.Name);
                        cmd.Parameters.AddWithValue("@tag", gang.Tag);
                        cmd.Parameters.AddWithValue("@description", gang.Description);
                        cmd.Parameters.AddWithValue("@money", 10);
                        cmd.Parameters.AddWithValue("@income", 1);
                        cmd.Parameters.AddWithValue("dateFounded", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //_gangs.Add(gang);
        }
    }
}
