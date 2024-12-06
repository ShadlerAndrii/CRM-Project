using Microsoft.EntityFrameworkCore;

namespace CRM_Project.Data
{
    public class AppDbContext : DbContext
    {
        public IConfiguration _configuration { get; set; }
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
    }
}
