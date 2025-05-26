using Microsoft.EntityFrameworkCore;
using DSAR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DSAR.Data 
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }
}