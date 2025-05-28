using DSAR.Models;
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
           var allusers = _userRepository.GetAll();

            return View(allusers);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _userRepository.GetById(id); // ✅ Use existing method

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserView
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserView user)
        {
            if (ModelState.IsValid)
            {
                var userEntity = new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName
                };
            }

            return View(user);
        }



        //public IActionResult Details(string id)
        //{
        //    if (string.IsNullOrEmpty(id)) return BadRequest();

        //    var user = _userRepository.GetById(id);
        //    if (user == null) return NotFound();

        //    return View(user);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _userRepository.Create(user);
            _userRepository.Save();
            return View();
        }

        public IActionResult Delete(string id)
        {
            
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