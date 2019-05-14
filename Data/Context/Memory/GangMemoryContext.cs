using System;
using Data.Interfaces;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace Data.Context
{
    public class GangMemoryContext : IGangContext
    {
        private static readonly List<Player> _players = new List<Player>
        {
            new Player(101, "Faker"),
            new Player(102, "Looking4Team"),
            new Player(103, "TheOddOne"),
        };

        private static readonly List<Player> _playersNaga = new List<Player>() {
            new Player(1, "FriendlyRob", 99, 875),
            new Player(2, "ION8", 2, 20),
            new Player(3, "Blitherjaguar45", 5, 45),
            new Player(4, "All Skill", 10, 140),
            new Player(5, "TAC Blinder", 8, 80),
            new Player(6, "KeizersPingu", 17, 185)
        };
        private static readonly List<Player> _playersExodia = new List<Player>()
        {
            new Player(7, "Dodging Dummy", 10, 150),
            new Player(8, "Daan Stylez", 99, 925),
            new Player(9, "Riron", 3, 10),
            new Player(10, "Nomis", 25, 325),
            new Player(11, "Vaips", 22, 295)
        };

        private static readonly List<Gang> _gangs = new List<Gang>()
        {
            new Gang(1, _playersNaga, "Zephyr Naga", "Naga", "One of the coolest teams in Zephyr", 12.50m, 0.50m, DateTime.Today),
            new Gang(2, _playersExodia, "Zephyr Exodia", "Zephyr", "Eerste Team Zephyr", 100.20m, 1.10m, new DateTime(2017, 2, 3))
        };

        public List<Gang> GetGangs()
        {
            return _gangs;
        }

        public IEnumerable<Player> GetPlayersInGang(int id)
        {
            Gang correctGang = null;
            foreach (Gang currentGang in _gangs)
            {
                if (currentGang.GangId == id)
                {
                    correctGang = currentGang;
                }
            }

            return correctGang?.Players;
        }

        // TODO: FIX THIS METHOD
        public void AddPlayerToGang(int playerId, int gangId)
        {
            _players.Add(_players[playerId]);
            
        }

        // TODO: Fix dit: zorgt dat het uit gangplayerlijst wordt verwijderd, niet uit lijst players
        public void RemovePlayerFromGang(int playerId, int gangId)
        {
            _players.Remove(_players[playerId]);

            Gang correctGang = null;
            foreach (Gang currentGang in _gangs)
            {
                if (currentGang.GangId == gangId)
                {
                    correctGang = currentGang;
                }
            }
            // TODO: Fix this stuff
            //Player correctPlayerId = null;
            //foreach (Player currentPlayer in correctGang.Players)
            //{
            //    if (currentPlayer.PlayerId == playerId)
            //    {
            //        correctPlayerId.PlayerId = currentPlayerId.PlayerId;
            //    }
            //}
            //correctGang.Players.Remove()
        }

        // TODO: Kijk of ik deze in de Gang of Player wil
        public List<Player> GetPlayersWithoutGang()
        {
            return _players;
        }

        public Gang GetGangWithId(int id)
        {
            Gang correctGang = null;
            foreach (Gang currentGang in _gangs)
            {
                if (currentGang.GangId == id)
                {
                    correctGang = currentGang;
                }
            }
            return correctGang;
        }

        public void UpdateGang(Gang gang)
        {
            Gang gangToUpdate = GetGangWithId(gang.GangId);
            gangToUpdate.Name = gang.Name;
            gangToUpdate.Tag = gang.Tag;
            gangToUpdate.Description = gang.Description;
        }

        public void RemoveGang(int id)
        {
            try
            {
                for (int i = 0; i < _gangs.Count; i++)
                {
                    if (_gangs[i].GangId == id)
                    {
                        _gangs.RemoveAt(i);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong while Removing a Gang!");
            }
        }

        public void CreateGang(Gang gang)
        {
            _gangs.Add(gang);
        }
    }

}