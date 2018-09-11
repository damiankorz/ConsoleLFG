using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleLFG.Models
{
    public class Lobby : BaseEntity 
    {
        public int ID {get; set;}
        public int UserID {get; set;}
        public User User {get; set;}
        
        [Required(ErrorMessage = "Game title is required")]
        [StringLength(45, ErrorMessage = "Game title cannot exceed 45 characters")]
        public string GameTitle {get; set;}
        public string Console {get; set;}

        [Required(ErrorMessage = "Gamertag is required")]
        [StringLength(45, ErrorMessage = "Gamertag cannot exceed 45 characters")]
        public string Gamertag {get; set;}
        public int NumPlayers {get; set;}
        public string Description {get; set;}
        public string Tags {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public Lobby()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}