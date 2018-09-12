using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ConsoleLFG.Models;

namespace ConsoleLFG.Controllers
{
    public class HomeController : Controller
    {
        //Database connection
        private LFGContext _dBContext; 
        public HomeController(LFGContext context)
        {
            _dBContext = context;
        }
        //Retrieve logged user
        public int? LoggedUser()
        {
            return HttpContext.Session.GetInt32("loggedUser");
        }
        //GET: /home
        [HttpGet("home")]
        public IActionResult Index()
        {
            if(LoggedUser() == null)
            {
                return RedirectToAction("Index", "User");
            }
            //Lobbies that are 2 or more hours old are considered expired and removed from the database
            List<Lobby> expiredLobbies = _dBContext.Lobbies
                                            .Where(lobby => lobby.CreatedAt < DateTime.Now.AddHours(-2))
                                            .ToList();          
            //Remove each expired lobby from database    
            foreach(Lobby lobby in expiredLobbies)
            {
                _dBContext.Remove(lobby); 
            }  
            _dBContext.SaveChanges(); 
            Lobby data = _dBContext.Lobbies
                            .Where(lobby => lobby.UserID == LoggedUser()).SingleOrDefault();
            return View(data);
        }
    }
}