using DSAR.Repositories;
using DSAR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Generic;
using System.Linq;

namespace DSAR.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult list()
        {
            var users = _userRepository.GetAll();

            var usersViewModel = users.Select(user => new UserView
            {
                //Fill in fields as needed
                Id = user.Id,  // Using IdentityUser's Id
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName
            }).ToList();

            return View(usersViewModel);
        }

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        //public IActionResult Edit(IdentityUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _userRepository.Update(user);
        //        _userRepository.Save();
        //        return RedirectToAction("list");
        //    }

        //    return View(user);
        //}

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                //_userRepository.Create(user);
                _userRepository.Save();
                return RedirectToAction("Insert");
            }

            return View(user);
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var sessionUserId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(sessionUserId))
            {
                return RedirectToAction("Login");
            }

            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string userId)
        {
            _userRepository.Delete(userId);
            _userRepository.Save();
            return RedirectToAction("list");
        }
    }
}