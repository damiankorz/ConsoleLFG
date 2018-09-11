using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ConsoleLFG.Models;

namespace ConsoleLFG.Controllers
{
    public class UserController : Controller
    {
        //Database connection
        private LFGContext _dBContext; 
        public UserController(LFGContext context)
        {
            this._dBContext = context;
        }
        //Function to check session of a logged user
        public IActionResult CheckSession(string view)
        {
            if(HttpContext.Session.GetInt32("loggedUser") == null)
            {
                return View(view);
            }
            return RedirectToAction("Index", "Home");
        }

        //GET: /
        [HttpGet("")]
        public IActionResult Index() => CheckSession("Index");
        //GET: /login
        [HttpGet("login")]
        public IActionResult Login() => CheckSession("Login");
        //POST: /login
        [HttpPost("login")]
        public IActionResult ProcessLogin(UserLogin model)
        {
            if(ModelState.IsValid)
            {
                List<User> users = _dBContext.Users.Where(u => u.Email == model.LoginEmail).ToList();
                if(users.Count == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid email/password combination");
                    return View("Login", model);
                }
                else
                {
                    //Check hashed password 
                    PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                    PasswordVerificationResult result = hasher.VerifyHashedPassword(model, users[0].Password, model.LoginPassword);
                    if(result == PasswordVerificationResult.Failed)
                    {
                        ModelState.AddModelError("LoginPassword", "Invalid email/password combination");
                        return View("Login", model);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("loggedUser", users[0].ID);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View("Login", model);
        }
        //GET: /register
        [HttpGet("register")]
        public IActionResult Register() => CheckSession("Register");
        //POST: /register
        [HttpPost("register")]
        public IActionResult ProcessRegister(UserRegister model)
        {
            if(ModelState.IsValid)
            {
                List<User> emails = _dBContext.Users.Where(u => u.Email == model.Email).ToList();
                if(emails.Count > 0)
                {
                    ModelState.AddModelError("Email", "Email already exits");
                    return View("Register", model);
                }
                else
                {
                    //Hash password 
                    PasswordHasher<UserRegister> hasher = new PasswordHasher<UserRegister>();
                    string hashedPassword = hasher.HashPassword(model, model.Password);
                    User newUser = new User
                    {
                        Email = model.Email,
                        Password = hashedPassword,
                    };
                    _dBContext.Add(newUser);
                    _dBContext.SaveChanges();
                    HttpContext.Session.SetInt32("loggedUser", newUser.ID);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Register", model);
        }
        //GET: /logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
