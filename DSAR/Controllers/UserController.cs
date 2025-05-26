using DSAR.Models;
using DSAR.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSAR.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }
        // GET: FunController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public async Task<ActionResult> Insert()
        {

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {

                return RedirectToAction("Login","User");
            
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId.Value);
            return View(user);
        }

        public async Task<IActionResult> list()
        {
            var users = await _context.User.ToListAsync();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
           // var currentUser = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);
           
          var usersViewModel =new List<UserView>();

            foreach (var user in users)
            {
                usersViewModel.Add(new UserView
                {
                    //Users = users,
                    // UserGUID = Guid.NewGuid().ToString(),
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email
                });

            }
            //var userView = new UserView
            //{
            //    //Users = users,
            //   // UserGUID = Guid.NewGuid().ToString(),
            //    UserId = currentUser.UserId,
            //    FullName = currentUser.FullName,
            //    Email = currentUser.Email
            //};

            return View(usersViewModel); // This passes both the list & current user
        }

        public async Task<IActionResult> Edit(int id)
        {
            var users = await _context.User.ToListAsync();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> update(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Update(user); // ✅ Update user
                await _context.SaveChangesAsync(); // ✅ Save changes
                return RedirectToAction("list"); // Go back to list
            }

            return View(user); // In case of validation errors
        }


        

        // GET: FunController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FunController/Create
        // ===========================
        // INSERT (Create Student)
        // ===========================

        // Show the form to create a new student
        public IActionResult Create()
        {
            return View();
        }

        // Handle POST: Save new student to the database
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Add(user); // Add to database
                await _context.SaveChangesAsync(); // Save changes
                return RedirectToAction("Insert"); // Redirect to empty form again
            }

            return View(user); // Return with validation errors
        }

        // GET: FunController/Edit/5


        // DELETE (Remove Student)
        // ===========================

        // Show confirmation page for deleting a student
        public async Task<IActionResult> Delete(int? id)
        {
            var users = await _context.User.ToListAsync();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int userid)
        {
            var user = await _context.User.FindAsync(userid);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("list"); // Redirect to user list after deletion
        }




        
       

       
    }
}
