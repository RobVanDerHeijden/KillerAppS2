using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using KillerAppS2.Models;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace KillerAppS2.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerLogic _playerLogic;

        public PlayerController(IPlayerContext playerContext)
        {
            _playerLogic = new PlayerLogic(playerContext);
        }

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
                }
            }
            else
            {
                TempData["LoginError"] = "Your login attempt was not successful. Please try again";
                return View(user);
            }

            return RedirectToAction("PlayerDashBoard");
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
                    playerLevel = (int) HttpContext.Session.GetInt32("PlayerLevel");
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
                playerId = (int) HttpContext.Session.GetInt32("PlayerId");
            }

            if (_playerLogic.HasEnoughEnergy(id, playerId))
            {
                // check if hack is success (based on difficulty and skill in category)
                if (_playerLogic.IsHackSuccessful(id, playerId))
                {
                    _playerLogic.GivePlayerReward(id, playerId);
                    _playerLogic.UpdatePlayerHackStats(id, playerId, true);
                    _playerLogic.UpdateSinglePlayerLevel(playerId);

                    UpdatePlayerDisplayableInformation(playerId);
                    TempData["HackCompleteNotice"] = "succes";
                }
                else
                {
                    _playerLogic.UpdatePlayerHackStats(id, playerId, false);
                    UpdatePlayerDisplayableInformation(playerId);
                    TempData["HackCompleteNotice"] = "failure";
                }
            }
            else
            {
                TempData["HackCompleteNotice"] = "notEnoughEnergy";
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
                    playerId = (int) HttpContext.Session.GetInt32("PlayerId");
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
                playerId = (int) HttpContext.Session.GetInt32("PlayerId");
            }

            // check if success (based on difficulty and skillpoints in category)
            if (_playerLogic.UpgradeSkill(id, playerId))
            {
                TempData["SkillUpgradeNotice"] = "succes";

                UpdatePlayerDisplayableInformation(playerId);
            }
            else
            {
                TempData["SkillUpgradeNotice"] = "failure";

                UpdatePlayerDisplayableInformation(playerId);
            }

            return RedirectToAction(actionName: "Skills", controllerName: "Player");
        }

        // Helper Method
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
            HttpContext.Session.SetString("ExperienceNeededForNextLevel", player.ExperienceNeededForNextLevel.ToString());
            HttpContext.Session.SetString("Money", player.Money.ToString());
            HttpContext.Session.SetString("Income", player.Income.ToString());
            HttpContext.Session.SetString("SkillPoints", player.SkillPoints.ToString());
            HttpContext.Session.SetString("Energy", player.Energy.ToString());
            HttpContext.Session.SetString("MaxEnergy", player.MaxEnergy.ToString());
            HttpContext.Session.SetString("EnergyRegen", player.EnergyRegen.ToString());
            HttpContext.Session.SetString("RefillableEnergy", player.RefillableEnergy.ToString());
        }

        public IActionResult RefillEnergy()
        {
            int playerId = 0;
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                playerId = (int) HttpContext.Session.GetInt32("PlayerId");
            }

            if (_playerLogic.UpdatePlayerEnergy(playerId))
            {   
                _playerLogic.UpdateSinglePlayerLevel(playerId);
                UpdatePlayerDisplayableInformation(playerId);
                TempData["EnergyCompleteNotice"] = "succes";
            }
            else
            {
                TempData["EnergyCompleteNotice"] = "failure";
            }
            PlayerViewModel PVmodel = new PlayerViewModel();
            PVmodel.Player = _playerLogic.GetPlayerWithId(playerId);
            
            return View("PlayerDashBoard", PVmodel);
        }

        public IActionResult Achievements()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                int playerId = 0;
                if (HttpContext.Session.GetInt32("PlayerLevel") != null)
                {
                    playerId = (int) HttpContext.Session.GetInt32("PlayerId");
                }

                AchievementViewModel AVmodel = new AchievementViewModel();
                AVmodel.Achievements = _playerLogic.GetPlayerAchievements(playerId);

                return View(AVmodel);
            }

            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult PlayerDashBoard()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                int playerId = 0;
                if (HttpContext.Session.GetInt32("PlayerLevel") != null)
                {
                    playerId = (int)HttpContext.Session.GetInt32("PlayerId");
                }

                PlayerViewModel PVmodel = new PlayerViewModel();
                PVmodel.Player = _playerLogic.GetPlayerWithId(playerId);

                return View(PVmodel);
            }

            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlayerDashBoard(int id, IFormCollection form)
        {
            int playerId = 0;
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                playerId = (int) HttpContext.Session.GetInt32("PlayerId");
            }
            Player player = new Player()
            {
                PlayerId = playerId,
                Username = form["Player.Username"],
                Password = form["Player.Password"],
                RealName = form["Player.RealName"],
                Country = form["Player.Country"],
                City = form["Player.City"]
            };

            _playerLogic.UpdatePlayerData(player);
            return RedirectToAction("Index");
        }

        public IActionResult DetailsPlayer(int id)
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                PlayerViewModel PVmodel = new PlayerViewModel();
                PVmodel.Player = _playerLogic.GetPlayerWithId(id);

                return View(PVmodel);
            }

            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult PlayerAchievements()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                AchievementViewModel AVmodel = new AchievementViewModel();
                AVmodel.Achievements = _playerLogic.GetAllPlayersAchievements();

                return View(AVmodel);
            }

            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult PlayerLevels()
        {
            if (HttpContext.Session.GetInt32("PlayerId") != null)
            {
                PlayerLevelViewModel PLVmodel = new PlayerLevelViewModel();
                PLVmodel.PlayerLevels = _playerLogic.GetAllPlayersLevels();
                
                return View(PLVmodel);
            }

            // Else
            TempData["LoginError"] = "You are not logged in. Please log in and try again";
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(IFormCollection form)
        {
            Player player = new Player
            {
                Username = form["Username"],
                Password = form["Password"],
                RealName = form["RealName"],
                Country = form["Country"],
                City = form["City"]
            };
            if (!_playerLogic.IsUsernameTaken(player.Username))
            {
                _playerLogic.RegisterUser(player);
                return View("Login");
            }
            // Else
            TempData["RegisterNotice"] = "taken";
            return View();
        }
    }
}