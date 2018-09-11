using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConsoleLFG.Models;

namespace ConsoleLFG.Controllers
{
    public class ListingController : Controller 
    {
        private LFGContext _dBContext;
        //Database connection
        public ListingController(LFGContext context)
        {
            this._dBContext = context;
        }
        //Retrieve logged user
        public int? LoggedUser()
        {
            return HttpContext.Session.GetInt32("loggedUser");
        }
        //GET: /lobbies
        [HttpGet("lobbies")]
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
            //Remove each expried lobby from database    
            foreach(Lobby lobby in expiredLobbies)
            {
                _dBContext.Remove(lobby); 
            }  
            _dBContext.SaveChanges();                   
            LobbyModels data = new LobbyModels
            {
                PlaystationLobbies = _dBContext.Lobbies
                                        .Where(lobby => lobby.Console == "Playstation")
                                        .Include(lobby => lobby.User)
                                        .OrderBy(lobby => lobby.CreatedAt)
                                        .ToList(),
                XboxLobbies = _dBContext.Lobbies
                                .Where(lobby => lobby.Console == "Xbox")
                                .Include(lobby => lobby.User)
                                .OrderBy(lobby => lobby.CreatedAt)
                                .ToList(),
                User = _dBContext.Users
                            .Where(user => user.ID == LoggedUser())
                            .SingleOrDefault(),
            };
            return View(data);
        }
        //POST: /lobbies/playstation
        [HttpPost("lobbies/playstation")]
        public IActionResult PlaystationJSON(string playstationTitle)
        {
            return Json(_dBContext.Lobbies
                            .Where(lobby => lobby.GameTitle == playstationTitle && lobby.Console == "Playstation")
                            .ToList());
        }
        //POST: /lobbies/xbox
        [HttpPost("lobbies/xbox")]
        public IActionResult XboxJSON(string xboxTitle)
        {   
            return Json(_dBContext.Lobbies
                            .Where(lobby => lobby.GameTitle == xboxTitle && lobby.Console == "Xbox")
                            .ToList());
        }
    }
}