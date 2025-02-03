using Microsoft.EntityFrameworkCore;
using Blood_Donation.Models; 
namespace Blood_Donation.Data 
{
    // Inherit from DbContext to manage your database operations
    public class ApplicationDbContext : DbContext
    {
        // Constructor to pass DbContextOptions to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Define DbSets for your models (these represent the tables in your database)
        public DbSet<RegisterDonor> DonorList { get; set; }  
    }
}
