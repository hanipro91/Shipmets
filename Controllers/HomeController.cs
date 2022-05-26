using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentTracking.Models;

namespace ShipmentTracking.Controllers
{
    public class HomeController : Controller
    {
        private Data data = new Data();
        private Customers LoggedUser;

        private Customers getCustomer(int id)
        {
            return (from U in data.Customers.Include(x => x.Shipments)
                    where U.ID == id
                    select U).FirstOrDefault();
        }

        private bool IsAuthenticated()
        {
            if (Request.Cookies.ContainsKey(Program.COOKIE_NAME))
            {
               LoggedUser = getCustomer(Convert.ToInt32(Request.Cookies[Program.COOKIE_NAME]));
               ViewData["User"] = LoggedUser.Name;
               return true;
            }
            return false;
        }

        public IActionResult Index(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if(id != null)
            {
                ViewData["Shipment"] = LoggedUser.Shipments.FirstOrDefault(s => s.ID == id);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customers c)
        {
            try
            {
                data.Customers.Add(c);
                data.SaveChanges();
                return Login(c.Email, c.Password);
            }
            catch(Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(c);
            }

        }

        public IActionResult Login()
        {
            int id = 0, adminId = 0;

            if (Request.Cookies.ContainsKey(Program.COOKIE_NAME))
                id = Convert.ToInt32(Request.Cookies[Program.COOKIE_NAME]);

            if (Request.Cookies.ContainsKey(Program.COOKIE_NAME_ADMIN))
                adminId = Convert.ToInt32(Request.Cookies[Program.COOKIE_NAME_ADMIN]);

            if (id > 0) ViewData["Customer"] = getCustomer(id);

            if (adminId > 0) ViewData["Admin"] = data.Users.FirstOrDefault(u => u.ID == adminId);
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(string user, string password)
        {
             int id = 0, adminId = 0;

            if (user != null)
                try
                {
                    user = user.ToLower();

                    id = (from U in data.Customers
                              where U.Email.ToLower() == user && U.Password == password
                              select U.ID).FirstOrDefault();

                    if(id <= 0)
                    adminId = (from U in data.Users
                                   where (U.Email.ToLower() == user || U.UserName.ToLower() == user) && U.Password == password
                                   select U.ID).FirstOrDefault();
                    if (id > 0)
                    {
                        Response.Cookies.Append(Program.COOKIE_NAME, id.ToString(),
                            new Microsoft.AspNetCore.Http.CookieOptions()
                            {
                                Expires = DateTime.Now.AddMonths(1)
                            });
                        return Redirect("~/");
                    }
                    else if(adminId > 0)
                    {
                        Response.Cookies.Append(Program.COOKIE_NAME_ADMIN, adminId.ToString(),
                            new Microsoft.AspNetCore.Http.CookieOptions()
                            {
                                Expires = DateTime.Now.AddMonths(1)
                            });
                        return Redirect("~/Adminisrator/");
                    }
                    else
                        ViewData["Error"] = "Invalid User";

                }
                catch (Exception e)
                {
                    ViewData["Error"] = e.Message;
                }

            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Append(Program.COOKIE_NAME, "0",
                new Microsoft.AspNetCore.Http.CookieOptions()
                { Expires = DateTime.Now.AddMonths(-10) });

            Response.Cookies.Append(Program.COOKIE_NAME_ADMIN, "0",
                new Microsoft.AspNetCore.Http.CookieOptions()
                { Expires = DateTime.Now.AddMonths(-10) });

            return RedirectToAction("Login");
        }

        public IActionResult Account()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View(LoggedUser);
        }

        public IActionResult EditAccount()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View(LoggedUser);
        }

        [HttpPost]
        public IActionResult EditAccount(Customers C)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            try
            {
                data.Customers.Update(C);
                data.SaveChanges();
                return RedirectToAction("Account");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View(C);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
