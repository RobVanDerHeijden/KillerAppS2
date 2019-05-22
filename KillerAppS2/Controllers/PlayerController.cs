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

        private readonly PlayerLogic _playerLogic = new PlayerLogic();
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
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                Player player = _playerLogic.Login(user.Username, user.Password);
                if (player != null)
                {
                    HttpContext.Session.SetInt32("PlayerId", player.PlayerId);
                    HttpContext.Session.SetString("Role", player.Role);
                    // Displayable information
                    HttpContext.Session.SetString("Username", player.Username);
                    HttpContext.Session.SetInt32("PlayerLevel", player.PlayerLevel);
                    HttpContext.Session.SetString("Experience", player.Experience.ToString());
                    HttpContext.Session.SetString("Money", player.Money.ToString());
                    HttpContext.Session.SetString("Income", player.Income.ToString());
                    HttpContext.Session.SetString("SkillPoints", player.SkillPoints.ToString());
                    HttpContext.Session.SetString("Energy", player.Energy.ToString());
                }
                else
                {
                    ViewData["LoginError"] = "Your login attempt was not successful. Please try again";
                    return View();
                }
            }
            return RedirectToAction("PlayerDashBoard");
        }

        public ActionResult PlayerDashBoard()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                return View();
            }
            // Else
            //ViewData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult PlayersWithoutGang()
        {
            IEnumerable<Player> players = _playerLogic.GetPlayersWithoutGang();

            return View(players);
        }
    }
}