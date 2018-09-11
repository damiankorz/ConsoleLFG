using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleLFG.Models
{
    public class LFGContext : DbContext
    {
        public LFGContext(DbContextOptions<LFGContext> options) : base(options) {}
        public DbSet<User> Users {get; set;}
        public DbSet<Lobby> Lobbies {get; set;}
    }
}