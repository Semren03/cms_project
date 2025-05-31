using cms_project.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
            {  

            }
        

        public DbSet<AttachmentComplaint> AttachmentComplaints { get; set; }

        public DbSet<Announcement> Announcements { get; set; }


        public DbSet<EmailSettings> EmailSettings { get; set; }

        public DbSet<Role> Roles { get; set; } 

        public DbSet<Claims> Claims { get; set; }

        public DbSet<ComplaintType> ComplaintTypes { get; set; }

        public DbSet<Status> Statuses { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.AssignedUser)
                .WithMany(u => u.AssignedUsers)
                .HasForeignKey(c => c.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.UserAccount)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
               .HasOne(c => c.Status)
               .WithMany(u => u.Complaints)
               .HasForeignKey(c => c.StatusId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAccount>()
                .HasOne(x => x.ComplaintType)
                .WithMany(c => c.UserAccounts)
                .HasForeignKey(c => c.ComplaintTypeResolverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComplaintHistory>()
                .HasOne(x => x.Complaint)
                .WithMany(c => c.ComplaintHistories)
                .HasForeignKey(x => x.ComplaintId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
           .HasOne(n => n.UserAccount)
           .WithMany(u => u.Notifications)
           .HasForeignKey(n => n.UserId)
           .OnDelete(DeleteBehavior.Cascade);
        }

        


    }
}
