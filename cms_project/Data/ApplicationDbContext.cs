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
        public DbSet<AttachmentComplaint> AttachmentComplaints { get; set; }

        public DbSet<Role> Roles { get; set; } 
        public DbSet<Claims> Claims { get; set; }
    }
}
