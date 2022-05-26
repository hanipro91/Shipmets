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
    public class ShipmentsController : Controller
    {
        private readonly Data _context;
        private Customers LoggedUser;

        private Dictionary<string, object> LoadSources()
        {
            var data = System.IO.File.ReadAllText("data/locations.json", System.Text.Encoding.UTF8);
            return (Dictionary<string, object>) 
                System.Text.Json.JsonSerializer.Deserialize(data, 
                                          typeof(Dictionary<string, object>));
            
        }

        private Customers getCustomer(int id)
        {
            return (from U in _context.Customers.Include(x=>x.Shipments)
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

        public ShipmentsController(Data context)
        {
            _context = context;
        }

        // GET: Shipments
        public  IActionResult Index()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
           
            try
            {
                ViewData["Count"] = LoggedUser.Shipments.Count;
            }
            catch
            {
                ViewData["Count"] = 0;
            }
            

            return View(LoggedUser.Shipments);
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var shipments = await _context.Shipments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shipments == null)
            {
                return NotFound();
            }

            return View(shipments);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            ViewData["Sources"] = LoadSources();
            ViewData["Country"] = LoggedUser.Country;
            ViewData["City"] = LoggedUser.City;
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Source,location,Date,ExepectedEstmatedTime,RealEstmatedTime,Weight,Price")] Shipments shipments)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                shipments.CustomersID = LoggedUser.ID;
                _context.Add(shipments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            
            ViewData["Error"] = "Unknown error occured!";
            
            return View(shipments);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }

            ViewData["Sources"] = LoadSources();
            ViewData["Country"] = LoggedUser.Country;
            ViewData["City"] = LoggedUser.City;
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Source,location,Date,ExepectedEstmatedTime,RealEstmatedTime,Weight,Price")] Shipments shipments)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id != shipments.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentsExists(shipments.ID))
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
            return View(shipments);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var shipments = await _context.Shipments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shipments == null)
            {
                return NotFound();
            }

            return View(shipments);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login", "Home");
            var shipments = await _context.Shipments.FindAsync(id);
            _context.Shipments.Remove(shipments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentsExists(int id)
        {
            return _context.Shipments.Any(e => e.ID == id);
        }
    }
}
