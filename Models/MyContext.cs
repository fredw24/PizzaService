using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace PizzaService.Models
{

    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> User {get; set;}
        public DbSet<Pizza> Pizza { get; set; }

        public void createUser(HttpContext context, User user)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            Add(user);
            SaveChanges();
            context.Session.SetInt32("id", user.UserId);
        }

    }
}