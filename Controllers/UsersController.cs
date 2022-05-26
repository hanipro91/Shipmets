using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShipmentTracking.Models;

namespace ShipmentTracking.Controllers
{
    public class UsersController : Controller
    {
        private readonly Data _context;
        private Users LoggedUser = null;
        private Users GetUser(int id)
        {
            return _context.Users.FirstOrDefault(U => U.ID == id); 
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

        public UsersController(Data context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,Password,Name,Gender,BirthDate,Email,Phone,Address,Picture")] Users users)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(users);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch(Exception ex)
                {
                    ViewData["Error"] = ex.Message;
                }
                
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,Password,Name,Gender,BirthDate,Email,Phone,Address,Picture")] Users users)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id != users.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");

            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult ValidateUniqUser([Bind("UserName")] string UserName)
        {
            try
            {
                UserName = UserName.ToLower();
                var u = _context.Users.Single(m => m.UserName.ToLower() == UserName);
                return Json(false);
            } catch (Exception)
            {
                return Json(true);
            }
        }

        [HttpPost]
        public JsonResult ValidateUniqEmail(string Email)
        {
            try
            {
                Email = Email.ToLower();
                var u = _context.Users.Single(m => m.Email.ToLower() == Email);
                return Json(false);
            }
            catch (Exception)
            {
                return Json(true);
            }
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
