﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace KillerAppS2.Models
{
    public class PlayerViewModel
    {
        public List<Player> Players { get; set; }

        public Player Player { get; set; }

        [Required(ErrorMessage = "Fill in username!")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
