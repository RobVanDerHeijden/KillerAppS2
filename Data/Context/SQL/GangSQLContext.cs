using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context.SQL
{
    class GangSQLContext : IGangContext
    {
        private DBConnection dbConnection = new DBConnection();

        public List<Gang> GetGangs()
        {
            List<Gang> gangs = new List<Gang>();
            try
            {
                using (SqlConnection con = dbConnection.SqlConnection)
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    
                    using (SqlCommand gangCmd = new SqlCommand("SELECT * FROM [dbo].[Gang];", con))
                    {
                        using (SqlDataReader gangReader = gangCmd.ExecuteReader())
                        {
                            while (gangReader.Read())
                            {
                                Gang gang = new Gang
                                {
                                    GangId = (int) gangReader["GangID"],
                                    Name = (string) gangReader["name"],
                                    Tag = (string) gangReader["tag"],
                                    Description = (string) gangReader["description"],
                                    Money = (decimal) gangReader["money"],
                                    Income = (decimal) gangReader["income"],
                                    DateFounded = (DateTime) gangReader["dateFounded"]
                                };

                                gang.Players = GetPlayersInGang(gang.GangId);
                                gangs.Add(gang);
                            }
                            
                        }
                    }
                }
                return gangs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<Player> GetPlayersInGang(int id)
        {
            List<Player> playerList = new List<Player>();
            try
            {
                using (SqlConnection con = dbConnection.SqlConnection)
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlCommand gangMemberCmd = 
                            //new SqlCommand("SELECT * FROM[dbo].[Player]", con))
                        new SqlCommand("SELECT P.* " +
                                       "FROM [dbo].[Player] P " +
                                       "INNER JOIN [dbo].[GangMember] GM ON GM.PlayerID = P.PlayerID " +
                                       "WHERE GangID = @id", con))
                    {
                        gangMemberCmd.Parameters.AddWithValue("@id", id);
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
                                    RealName = (string)gangMemberReader["realName"],
                                    Country = (string)gangMemberReader["country"],
                                    City = (string)gangMemberReader["city"]
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

        public Gang GetGangWithId(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateGang(Gang gang)
        {
            throw new NotImplementedException();
        }

        public void RemoveGang(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateGang(Gang gang)
        {
            throw new NotImplementedException();
        }
    }
}
