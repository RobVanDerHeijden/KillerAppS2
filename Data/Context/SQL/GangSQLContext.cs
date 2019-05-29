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
        private readonly DBConnection _dbConnection = new DBConnection();

        public List<Gang> GetGangs()
        {
            List<Gang> gangs = new List<Gang>();
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
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

            foreach (Gang currentGang in gangs)
            {
                currentGang.Players = GetPlayersInGang(currentGang.GangId);
            }
            return gangs;
        }

        public IEnumerable<Player> GetPlayersInGang(int gangId)
        {
            try
            {
                List<Player> playerList = new List<Player>();
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand gangMemberCmd = 
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
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    // TODO: FIX DEZE STUFF
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ", conn))
                    {
                        
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // TODO: Check if player is last player: if yes-> also delete Gang
        public void RemovePlayerFromGang(int playerId, int gangId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmdGangMember = new SqlCommand("DELETE FROM [dbo].[GangMember] WHERE GangId = @GangId AND PlayerId = @PlayerId", conn))
                    {
                        cmdGangMember.Parameters.AddWithValue("@GangId", gangId);
                        cmdGangMember.Parameters.AddWithValue("@PlayerId", playerId);
                        
                        cmdGangMember.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // TODO: Check if player is last player: if yes-> also delete Gang
        // TODO: opschonen: player kan maar in een gang zitten dus heb geen WHERE condition voor GangId nodig(oftewel alleen delete from GM where playerid)
        public void RemovePlayerFromGang(int playerId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    int gangId;
                    using (SqlCommand cmdPlayerWhatGang = new SqlCommand("SELECT GangId FROM [dbo].[GangMember] WHERE PlayerId = @PlayerId", conn))
                    {
                        cmdPlayerWhatGang.Parameters.AddWithValue("@PlayerId", playerId);
                        gangId = (int) cmdPlayerWhatGang.ExecuteScalar();
                    }


                    using (SqlCommand cmdGangMember = new SqlCommand("DELETE FROM [dbo].[GangMember] WHERE GangId = @GangId AND PlayerId = @PlayerId", conn))
                    {
                        cmdGangMember.Parameters.AddWithValue("@GangId", gangId);
                        cmdGangMember.Parameters.AddWithValue("@PlayerId", playerId);

                        cmdGangMember.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public Gang GetGangWithId(int gangId)
        {
            try
            {
                Gang gang = new Gang();
                using (SqlConnection conn = _dbConnection.GetConnString())
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
                using (SqlConnection conn = _dbConnection.GetConnString())
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

        // Removes All GangMembers from Gang, THEN removes the Gang itself
        public void RemoveGang(int gangId)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmdGangMember = new SqlCommand("DELETE FROM [dbo].[GangMember] WHERE GangId = @GangId", conn),
                        cmdGang = new SqlCommand("DELETE FROM [dbo].[Gang] WHERE GangId = @GangId", conn))
                    {
                        cmdGangMember.Parameters.AddWithValue("@GangId", gangId);
                        cmdGang.Parameters.AddWithValue("@GangId", gangId);
                        cmdGangMember.ExecuteNonQuery();
                        cmdGang.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Creates Gang, [Then Adds current user as GangMember With Leader Role]
        // TODO: Add current player to gang as leader when gang is created. Maar eerst sessions en inloggen regelen
        public void CreateGang(Gang gang)
        {
            try
            {
                using (SqlConnection conn = _dbConnection.GetConnString())
                {
                    conn.Open();
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
        }
    }
}
