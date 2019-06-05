using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class PlayerMemoryContext// : IPlayerContext
    {
        private List<Player> _players = new List<Player>
        {
            new Player(101, "Faker"),
            new Player(102, "Looking4Team"),
            new Player(103, "TheOddOne"),
        };

        public int GetLevel(int playerId)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayers()
        {
            if (_players == null)
            {
                _players = new List<Player>
                {
                    new Player(201, "aaFaker"),
                    new Player(202, "aaLooking4Team"),
                    new Player(203, "aaTheOddOne"),
                    new Player(204, "bbFaker"),
                    new Player(205, "bbLooking4Team"),
                    new Player(206, "bbTheOddOne"),
                };
            }
            return _players;
        }

        public List<Player> GetPlayersWithoutGang()
        {
            throw new NotImplementedException();
            //return _players;
        }

        public Player GetPlayerWithId(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayer(int id)
        {
            throw new NotImplementedException();
        }

        public void CreatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Player Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<Hack> GetAvailableHacks(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerLevels()
        {
            throw new NotImplementedException();
        }

        public bool PassedDifficultyCheck(int hackId, int playerId)
        {
            throw new NotImplementedException();
        }

        public void GivePlayerReward(int id, int playerId)
        {
            throw new NotImplementedException();
        }

        public int CalculatePlayerLevel(int experience)
        {
            throw new NotImplementedException();
        }
    }
}
