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
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                IEnumerable<Player> players = _playerLogic.GetPlayers();

                return View(players);
            }
            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
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
                    UpdatePlayerDisplayableInformation(player.PlayerId);
                    //HttpContext.Session.SetInt32("PlayerId", player.PlayerId);
                    //HttpContext.Session.SetString("Role", player.Role);
                    //// Displayable information
                    //HttpContext.Session.SetString("Username", player.Username);
                    //HttpContext.Session.SetInt32("PlayerLevel", player.PlayerLevel);
                    //HttpContext.Session.SetString("Experience", player.Experience.ToString());
                    //HttpContext.Session.SetString("Money", player.Money.ToString());
                    //HttpContext.Session.SetString("Income", player.Income.ToString());
                    //HttpContext.Session.SetString("SkillPoints", player.SkillPoints.ToString());
                    //HttpContext.Session.SetString("Energy", player.Energy.ToString());
                }
            }
            else
            {
                TempData["LoginError"] = "Your login attempt was not successful. Please try again";
                return View(user);
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
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult PlayersWithoutGang()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                PlayerViewModel PVmodel = new PlayerViewModel();
                PVmodel.Players = _playerLogic.GetPlayersWithoutGang();

                return View(PVmodel);
            }
            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult Hacks()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                int playerLevel = 0;
                if (HttpContext.Session.GetInt32("PlayerLevel") != null)
                {
                    playerLevel = (int)HttpContext.Session.GetInt32("PlayerLevel");
                }

                HackViewModel HVmodel = new HackViewModel();
                HVmodel.Hacks = _playerLogic.GetAvailableHacks(playerLevel);

                return View(HVmodel);
            }
            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult DoHack(int id)
        {
            int playerId = 0;
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                playerId = (int)HttpContext.Session.GetInt32("PlayerId");
            }
            // check if succes (based on difficulty and skill in category)
            if (_playerLogic.IsHackSuccessful(id, playerId)) // TODO: Need to check if there is enough energy
            {
                _playerLogic.GivePlayerReward(id, playerId);
                //_playerLogic.ConsumeEnergy(id, playerId);
                _playerLogic.UpdatePlayerLevels();
                //_playerLogic.UpdatePlayerHackStats(id, playerId);

                UpdatePlayerDisplayableInformation(playerId);
                TempData["HackCompleteNotice"] = "succes"; // TODO: Add the random number / what it needed to hit + Checkmark eg.: 74 / 15
            }
            else
            {
                TempData["HackCompleteNotice"] = "failure";
            }

            return RedirectToAction(actionName: "Hacks", controllerName: "Player");
        }

        public IActionResult Skills()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                int playerId = 0;
                if (HttpContext.Session.GetInt32("PlayerLevel") != null)
                {
                    playerId = (int)HttpContext.Session.GetInt32("PlayerId");
                }

                SkillViewModel SVmodel = new SkillViewModel();
                SVmodel.Skills = _playerLogic.GetPlayerSkills(playerId);

                return View(SVmodel);
            }
            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult UpgradeSkill(int id)
        {
            int playerId = 0;
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                playerId = (int)HttpContext.Session.GetInt32("PlayerId");
            }
            // check if succes (based on difficulty and skill in category)
            if (_playerLogic.UpgradeSkill(id, playerId))
            {
                TempData["SkillUpgradeNotice"] = "succes";

                UpdatePlayerDisplayableInformation(playerId);
            }
            else
            {
                TempData["SkillUpgradeNotice"] = "failure";
            }

            return RedirectToAction(actionName: "Skills", controllerName: "Player");
        }

        // Helper Methods
        public void UpdatePlayerDisplayableInformation(int playerId)
        {
            Player player = _playerLogic.GetPlayerWithId(playerId);
            // Validation Data
            HttpContext.Session.SetInt32("PlayerId", player.PlayerId);
            HttpContext.Session.SetString("Role", player.Role);
            // Displayable Data
            HttpContext.Session.SetString("Username", player.Username);
            HttpContext.Session.SetInt32("PlayerLevel", player.PlayerLevel);
            HttpContext.Session.SetString("Experience", player.Experience.ToString());
            HttpContext.Session.SetString("Money", player.Money.ToString());
            HttpContext.Session.SetString("Income", player.Income.ToString());
            HttpContext.Session.SetString("SkillPoints", player.SkillPoints.ToString());
            HttpContext.Session.SetString("Energy", player.Energy.ToString());
        }

        public IActionResult RefillEnergy()
        {
            return View("PlayerDashBoard");
        }
    }
}