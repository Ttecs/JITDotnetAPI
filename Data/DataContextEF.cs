using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class DataContextEF:DbContext
    {
        private readonly IConfiguration _config;
        public DataContextEF(IConfiguration config) {
            _config = config;
        }

        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<UserSalary> UserSalary { get; set; }
        //public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<ClassRoom> Classrooms { get; set; }
        public virtual DbSet<AllocateSubject> AllocateSubjects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilde)
        {
            if (!optionsBuilde.IsConfigured)
            {
                optionsBuilde
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilde => optionsBuilde.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<User>().ToTable("Users").HasKey(u=>u.UserId);
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectID);
                entity.Property(e => e.SubjectName).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<AllocateSubject>()
           .HasKey(x => new { x.TeacherID, x.SubjectID });

            modelBuilder.Entity<AllocateSubject>()
                .HasOne(a => a.Teacher)
                .WithMany(t => t.AllocateSubjects)
                .HasForeignKey(a => a.TeacherID);

            modelBuilder.Entity<AllocateSubject>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.AllocateSubjects)
                .HasForeignKey(a => a.SubjectID);
            //modelBuilder.Entity<Subject>().HasKey(u => u.SubjectID);
            //modelBuilder.Entity<UserJobInfo>().HasKey(u => u.UserId);

        }
    }
}
