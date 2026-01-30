using Microsoft.AspNetCore.Mvc;
using Resturant_Menu.Data;
using Resturant_Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Resturant_Menu.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminDashboardController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "AdminAccount");

            var stats = new
            {
                TotalBookings = _db.Bookings.Count(),
                TotalMenuItems = _db.MenuItems.Count(),
                TotalCategories = _db.Categories.Count(),
                TotalAdmins = _db.Admins.Count()
            };

            return View(stats);
        }

        // Categories CRUD
        public IActionResult Categories()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "AdminAccount");

            var categories = _db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory() => View();

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _db.Categories.Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }
            return RedirectToAction("Categories");
        }

        // Menu Items CRUD
        public IActionResult MenuItems()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "AdminAccount");

            var menuItems = _db.MenuItems.Include(m => m.Category).ToList();
            return View(menuItems);
        }

        [HttpGet]
        public IActionResult CreateMenuItem()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateMenuItem(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _db.MenuItems.Add(menuItem);
                _db.SaveChanges();
                return RedirectToAction("MenuItems");
            }
            ViewBag.Categories = _db.Categories.ToList();
            return View(menuItem);
        }

        [HttpGet]
        public IActionResult EditMenuItem(int id)
        {
            var menuItem = _db.MenuItems.Find(id);
            if (menuItem == null) return NotFound();
            ViewBag.Categories = _db.Categories.ToList();
            return View(menuItem);
        }

        [HttpPost]
        public IActionResult EditMenuItem(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _db.MenuItems.Update(menuItem);
                _db.SaveChanges();
                return RedirectToAction("MenuItems");
            }
            ViewBag.Categories = _db.Categories.ToList();
            return View(menuItem);
        }

        public IActionResult DeleteMenuItem(int id)
        {
            var menuItem = _db.MenuItems.Find(id);
            if (menuItem != null)
            {
                _db.MenuItems.Remove(menuItem);
                _db.SaveChanges();
            }
            return RedirectToAction("MenuItems");
        }

        // Admins CRUD
        public IActionResult Admins()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "AdminAccount");

            var admins = _db.Admins.ToList();
            return View(admins);
        }

        [HttpGet]
        public IActionResult CreateAdmin() => View();

        [HttpPost]
        public IActionResult CreateAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _db.Admins.Add(admin);
                _db.SaveChanges();
                return RedirectToAction("Admins");
            }
            return View(admin);
        }

        [HttpGet]
        public IActionResult EditAdmin(int id)
        {
            var admin = _db.Admins.Find(id);
            if (admin == null) return NotFound();
            return View(admin);
        }

        [HttpPost]
        public IActionResult EditAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _db.Admins.Update(admin);
                _db.SaveChanges();
                return RedirectToAction("Admins");
            }
            return View(admin);
        }

        public IActionResult DeleteAdmin(int id)
        {
            var admin = _db.Admins.Find(id);
            if (admin != null)
            {
                _db.Admins.Remove(admin);
                _db.SaveChanges();
            }
            return RedirectToAction("Admins");
        }
    }
}