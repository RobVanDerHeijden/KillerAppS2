using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace KillerAppS2.Controllers
{
    public class GangController : Controller
    {
        private readonly GangLogic _gangLogic = new GangLogic();
        // GET: Gang
        public ActionResult Index()
        {
            IEnumerable<Gang> gangs = _gangLogic.GetGangs();

            return View(gangs);
        }

        public IActionResult DetailsGang(int id)
        {
            return View(_gangLogic.GetGangWithId(id));
        }

        public IActionResult EditGang(int id)
        {
            return View(_gangLogic.GetGangWithId(id));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGang(int id, IFormCollection form)
        {
            Gang gang = new Gang()
            {
                Name = form["Name"],
                Tag = form["Tag"],
                Description = form["Description"],
                GangId = id
            };
            _gangLogic.UpdateGang(gang);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteGang(int id)
        {
            _gangLogic.RemoveGang(id);
            return RedirectToAction("Index");
        }

        public IActionResult CreateGang()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGang(IFormCollection form)
        {
            // AI TaakId
            List<Gang> allGangs = _gangLogic.GetGangs();
            int autoIncrementHighestId = 1;
            foreach (Gang currentGang in allGangs)
            {
                if (currentGang.GangId > autoIncrementHighestId)
                {
                    autoIncrementHighestId = currentGang.GangId + 1;
                }
            }

            Gang gang = new Gang
            {
                Name = form["Name"],
                Tag = form["Tag"],
                Description = form["Description"],
                Players = new List<Player>(),
                GangId = autoIncrementHighestId
            };
            _gangLogic.CreateGang(gang);
            return RedirectToAction("Index");
        }

        public IActionResult RemovePlayerFromGang(int id)
        {
            _gangLogic.RemovePlayerFromGang(id);
            return RedirectToAction("DetailsGang");
        }

        public IActionResult InvitePlayer()
        {
            throw new NotImplementedException();
        }
    }
}