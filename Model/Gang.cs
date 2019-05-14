using System;
using System.Collections.Generic;

namespace Model
{
    public class Gang
    {
        public int GangId { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public decimal Money { get; set; }
        public decimal Income { get; set; }
        public DateTime DateFounded { get; set; }

        public Gang()
        {
            
        }

        public Gang(int gangId, IEnumerable<Player> players, string name, string tag, string description, decimal money, decimal income, DateTime dateFounded)
        {
            GangId = gangId;
            Players = players;
            Name = name;
            Tag = tag;
            Description = description;
            Money = money;
            Income = income;
            DateFounded = dateFounded;
        }
    }
}