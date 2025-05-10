using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyProject
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class UserModel()
    {
        [Key]
        public required Guid user_id { get; set; }
        public required string name { get; set; }
        public int? age { get; set; }

    }
}
