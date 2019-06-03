using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace KillerAppS2.Models
{
    public class HackViewModel
    {
        public List<Hack> Hacks { get; set; }

        public Hack Hack { get; set; }
    }
}
