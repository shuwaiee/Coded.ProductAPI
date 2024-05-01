using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models.Entites
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }
        public DbSet<BankBranchEntity> BankBranches { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<UserAccountEntity> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankBranchEntity>().HasData(new BankBranchEntity
            {
                Id = 1,
                Name = "Kifan",
                Location = "https://map.google.com"
            });
            modelBuilder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
            {
                Id = 1,
                Name = "Mohammad",
                WorkPlaceId = 1
            });
            base.OnModelCreating(modelBuilder);
        }
    }
    public class BankBranchEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? BranchManager { get; set; }
        public List<EmployeeEntity> Employees { get; set; }
    }
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BankBranchEntity WorkPlace { get; set; }
        public int WorkPlaceId { get; set; }
    }
    public class UserAccountEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; private set; }
        public string Email { get; set; }
        public string CivilId { get; set; }
        public bool IsAdmin { get; set; }

        private UserAccountEntity()
        {

        }
        public static UserAccountEntity Create(string username, string password, bool isAdmin)
        {
            return new UserAccountEntity
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                IsAdmin = isAdmin,
            };
        }
    }
}
