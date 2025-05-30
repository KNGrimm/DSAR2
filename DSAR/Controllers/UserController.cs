using DSAR.Models;
using DSAR.Repositories;
using DSAR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

       
        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult list()
        {
            var users = _userRepository.GetAll();

            var viewModel = users.Select(u => new UserView
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                UserName = u.UserName
            }).ToList();

            return View(viewModel); // ✅ Now matches @model List<UserView>
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
        public IActionResult Edit(UserView user)
        {
            if (ModelState.IsValid)
            {
                var userEntity = new User
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName
                };

                try
                {
                    _userRepository.Update(userEntity); // ✅ Safe update
                    return RedirectToAction("List");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(user); // Return with errors if any
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

        //public IActionResult Delete(string id)
        //{

        //    var user = _userRepository.GetById(id);
        //    if (user == null) return NotFound();

        //    return View(user);
        //}
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Delete(string Id)
        {
            try
            {
                _userRepository.Delete(Id); // ✅ Safe tracked delete
                return RedirectToAction("List");
            }
            catch (InvalidOperationException ex)
            {
                // Optionally show an error message
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("List");
            }
        }
    }
}