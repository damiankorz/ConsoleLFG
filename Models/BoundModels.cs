using System;
using System.Collections.Generic;

namespace ConsoleLFG.Models
{
    public class HomeModels : BaseEntity
    {
        public User User {get; set;}
        public Lobby Lobby {get; set;}
    }
    public class LobbyModels : BaseEntity
    {
        public List<Lobby> PlaystationLobbies {get; set;}
        public List<Lobby> XboxLobbies {get; set;}
        public User User {get; set;}
    }
}