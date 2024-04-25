using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            
        }
        public DbSet<BankBranch> BankBranches { get; set; }    
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankBranch>().HasData(new BankBranch
            {
                Id = 1,
                Name = "Kifan",
                Location = "https://map.google.com"
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Mohammad",
                WorkPlaceId = 1
            });
            base.OnModelCreating(modelBuilder);
        }
    }
    public class BankBranch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? BranchManager { get; set; }
        public List<Employee> Employees { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BankBranch WorkPlace { get; set; }
        public int WorkPlaceId { get; set; }
    }
}
