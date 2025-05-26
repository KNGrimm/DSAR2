
﻿using DSAR.ViewModels;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace DSAR.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            HttpContext.Session.Clear();

            // Prevent browser from caching the page
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }

        public async Task<IActionResult> Main()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId.Value);


            UserView userView = new UserView
            {
                FullName = user.FullName,
            };
            return View(userView);

        }
        [HttpPost]
        public IActionResult Logout()
        {
            // ✅ Clear the session
            HttpContext.Session.Clear();

            // Prevent browser from caching the page
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("Login");
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(int userid, string password)
        {
            if (userid == 0 || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "ID and Password are required.");
                return View();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(s => s.UserId == userid && s.Password == password);

            if (user != null)
            {
                // ✅ Store user ID in session
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Main");
            }

            ModelState.AddModelError("", "Invalid ID or password.");
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
