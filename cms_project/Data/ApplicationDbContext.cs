using cms_project.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
            {  

            }
       public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
    }
}
