using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillerAppS2.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace KillerAppS2.Controllers
{
    public class PlayerController : Controller
    {

        private PlayerLogic _playerLogic = new PlayerLogic();
        public ActionResult Index()
        {
            IEnumerable<Player> players = _playerLogic.GetPlayers();

            return View(players);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            return RedirectToAction(actionName: "Index");
        }
        
        public IActionResult Logout()
        {
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

    }
}