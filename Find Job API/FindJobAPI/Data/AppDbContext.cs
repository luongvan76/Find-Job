using FindJobAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Data
{
    public class AppDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        } // constructor

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<seeker>()
              .HasOne(s => s.account)
              .WithMany(a => a.seekers)
              .HasForeignKey(s => s.UID)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<employer>()
                .HasOne(s => s.account)
                .WithMany(s => s.employers)
                .HasForeignKey(s => s.UID);
            modelBuilder.Entity<recruitment>()
                .HasKey(r => new { r.UID, r.job_id });
            modelBuilder.Entity<recruitment>()
                .HasOne(r => r.seeker)
                .WithMany(r => r.recruitments)
                .HasForeignKey(r => r.UID);
            modelBuilder.Entity<recruitment>()
               .HasOne(r => r.job)
               .WithMany(r => r.recruitment)
               .HasForeignKey(r => r.job_id);
            modelBuilder.Entity<job>()
                .HasOne(j => j.employer)
                .WithMany(j => j.jobs)
                .HasForeignKey(j => j.UID);
            modelBuilder.Entity<job>()
               .HasOne(j => j.type)
               .WithMany(j => j.jobs)
               .HasForeignKey(j => j.type_id);
            modelBuilder.Entity<job>()
              .HasOne(j => j.industry)
              .WithMany(j => j.job)
              .HasForeignKey(j => j.industry_id);
            modelBuilder.Entity<recruitment_no_account>()
              .HasOne(j => j.job)
              .WithMany(j => j.recruitment_no_account)
              .HasForeignKey(j => j.job_id);
        }

        public DbSet<account> Account { get; set; }
        public DbSet<job> Job { get; set; }
        public DbSet<employer> Employer { get; set; }
        public DbSet<seeker> Seeker { get; set; }
        public DbSet<industry> Industry { get; set; }
        public DbSet<recruitment_no_account> Recruitment_No_Accounts { get; set; }
        public DbSet<recruitment> Recruitment { get; set; }
        public DbSet<type> Type { get; set; }
    }
}