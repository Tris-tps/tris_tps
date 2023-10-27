using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketTrisServer.Model;

namespace WebSocketTrisServer.Data
{
    public class LoginDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public string DbPath { get; }
        public LoginDbContext()
        {
            var folder = AppContext.BaseDirectory;
            var path = Path.Combine(folder, "..\\..\\..\\UsersCredentials.db");
            DbPath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Username);
        }
    }
}
