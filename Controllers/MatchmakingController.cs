using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ConsoleLFG.Models;

namespace ConsoleLFG.Controllers
{
    public class MatchmakingController : Controller
    {
        private LFGContext _dBContext;
        //Database connection
        public MatchmakingController(LFGContext context)
        {
            this._dBContext = context;
        }
        //Retrieve logged user
        public int? LoggedUser()
        {
            return HttpContext.Session.GetInt32("loggedUser");
        }
        //GET: /matchmaking
        [HttpGet("matchmaking")]
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
            return View();
        }
        //POST: /matchmaking/create
        [HttpPost("matchmaking/create")]
        public IActionResult CreateLobby(Lobby model, List<string> allTags)
        {
            if(LoggedUser() == null)
            {
                return RedirectToAction("Index", "User");
            }
            else 
            {
                if(ModelState.IsValid)
                {
                    //Only allow user to create one lobby
                    List<Lobby> userLobbies = _dBContext.Lobbies
                                                .Where(lobby => lobby.UserID == LoggedUser())
                                                .ToList();
                    if(userLobbies.Count > 0)
                    {
                        ModelState.AddModelError("Game Title", "Remove current lobby before creating a new one");
                        return View("Index", model);
                    }
                    else
                    {
                        //Format list all tags into one string 
                        string tags = string.Join(" ", allTags);
                        Lobby newLobby = new Lobby
                        {
                            UserID = (int)LoggedUser(),
                            GameTitle = model.GameTitle,
                            Console = model.Console,
                            Gamertag = model.Gamertag,
                            NumPlayers = model.NumPlayers,
                            Description = model.Description,
                            Tags = tags,
                        };
                        _dBContext.Add(newLobby);
                        _dBContext.SaveChanges();
                        return RedirectToAction("Index", "Listing");
                    }
                }
                return View("Index", model);
            }
        }
        //GET: /matchmaking/id/remove
        [HttpGet("matchmaking/{id}/remove")]
        public IActionResult RemoveLobby(int id)
        {
            Lobby lobbyToRemove = _dBContext.Lobbies.Where(lobby => lobby.ID == id).SingleOrDefault();
            _dBContext.Remove(lobbyToRemove);
            _dBContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}