using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipmentTracking.Models;

namespace ShipmentTracking.Controllers
{
    public class AdminisratorController : Controller
    {
        private Data data = new Data();
        private Users LoggedUser = null;

        private Users GetUser(int id)
        {
            return data.Users.FirstOrDefault(U => U.ID == id);
        }

        private bool IsAuthenticated()
        {
            if (Request.Cookies.ContainsKey(Program.COOKIE_NAME_ADMIN))
            {
                LoggedUser = GetUser(Convert.ToInt32(Request.Cookies[Program.COOKIE_NAME_ADMIN]));
                ViewData["User"] = LoggedUser.Name;
                return true;
            }
            return false;
        }

        public IActionResult Index()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            return View(LoggedUser);
        }

        public IActionResult Shipments()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            return View(data.Shipments.ToList());
        }

        public IActionResult EditShipment(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            return View(data.Shipments.FirstOrDefault(s=>s.ID == id));
        }

        [HttpPost]
        public IActionResult EditShipment(int id, int RealEstmatedTime)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            Shipments shipment = data.Shipments.FirstOrDefault(s => s.ID == id);

            if (ModelState.IsValid)
                try
                {
                    shipment.RealEstmatedTime = RealEstmatedTime;
                    data.Update(shipment);
                    data.SaveChanges();
                    return RedirectToAction("Shipments");
                }
                catch (Exception)
                {
                    throw;
                }
           

            return View(shipment);
        }

        public IActionResult ShipmentDetails(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            Shipments shipment = data.Shipments.FirstOrDefault(s => s.ID == id);
            return View(shipment);
        }

        public IActionResult DeleteShipment(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            Shipments shipment = data.Shipments.FirstOrDefault(s => s.ID == id);
            return View(shipment);
        }

        [HttpPost]
        public IActionResult DeleteShipmentConfirmed(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            Shipments shipment = data.Shipments.FirstOrDefault(s => s.ID == id);
          
            data.Shipments.Remove(shipment);
            data.SaveChanges();
            return RedirectToAction("Shipments");
        }
    }
}
