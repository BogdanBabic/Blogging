﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Blogging.Models;
using Blogging.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blogging.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Blogging.TagHelpers;

namespace Blogging.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly INotyfService _notyf;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserRepository userRepository, INotyfService notyf, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _notyf = notyf;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            var user = _userRepository.GetUserByUsername(registerViewModel.Username);

            User userVM = new User
            {
                UserId = registerViewModel.UserId,
                Username = registerViewModel.Username,
                Password = registerViewModel.Password,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email
            };

            if (!ModelState.IsValid)
            {

                if (user == null)
                {
                    _userRepository.CreateUser(userVM);
                    _notyf.Success("Uspešno ste kreirali nalog");
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    _notyf.Error("Korisničko ime već postoji");
                    return View("Registration", userVM);
                }

            }

            else
            {
                return View("Registration", userVM);
            }
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        public IActionResult SignIn(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginViewModel);
            }

            var user = _userRepository.GetUserByUsername(loginViewModel.Username);

            if (user != null)
            {
                var isPasswordOk = EncryptionHelper.Encrypt(loginViewModel.Password) == user.Password ? true : false;

                if (isPasswordOk)
                {
                    user.Password = "";
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(7);
                    var serializedUser = JsonConvert.SerializeObject(user);
                    Response.Cookies.Append("User", serializedUser, cookieOptions);

                    _notyf.Success("Uspešno ste se ulogovali!");

                    return RedirectToAction("Index", "Home");
                }
            }

            _notyf.Error("Uneli ste neispravne podatke");

            return View("Login");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("User");

            _notyf.Success("Uspešno ste se odjavili");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UserProfile()
        {
            User user = new User();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userCookie = HttpContext!.Request.Cookies["User"];
                user = JsonConvert.DeserializeObject<User>(userCookie)!;
            }

            

            var vm = new UpdateUserViewModel()
            {
                UserId = user.UserId,
                Username = user.Username
            };

            return View(vm);
        }

        public IActionResult EditPassword(UpdateUserViewModel model)
        {
            var user = _userRepository.GetUserById(model.UserId);

            if (model.Username != user.Username)
            {
                return View("UserProfile", model);
            }

            if (user.Password == EncryptionHelper.Encrypt(model.CurrentPassword))
            {
                _userRepository.UpdatePassword(user, model.NewPassword);

                _notyf.Success("Uspešno ste promenili šifru");

                return RedirectToAction("Index", "Home");
            }

            else
            {
                _notyf.Error("Šifra je neispravna");

                return View("UserProfile", model);
            }
        }

        [TypeFilter(typeof(UserExceptionFilter))]
        public IActionResult MyPosts()
        {
            throw new InvalidOperationException("Neka greška");

            return View();
        }
    }
}
