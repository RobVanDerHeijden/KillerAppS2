using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace KillerAppS2.Models
{
    public class GangViewModel
    {
        //[Required]
        public List<Player> Players { get; set; }
        public List<Gang> Gangs { get; set; }
        public List<Player> PlayersWithoutGang{ get; set; }
        public int GangId { get; set; }
    }
}
