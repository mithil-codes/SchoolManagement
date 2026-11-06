using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SchoolManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Fee> Fees { get; set; }

        public DbSet<Notice> Notices { get; set; }

        public DbSet<AdmissionApplication> AdmissionApplications { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // Identity Table Configuration

            // RelationShip Configuration

            builder.Entity<ClassSubject>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ClassSubject>()
              .HasOne(cs => cs.Subject)
              .WithMany(s => s.ClassSubjects)
              .HasForeignKey(cs => cs.SubjectId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Student>()
    .HasOne(s => s.ApplicationUser)
    .WithOne()                                    
    .HasForeignKey<Student>(s => s.ApplicationUserId)
    .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Student>()
    .HasIndex(s => s.AdmissionNumber)
    .IsUnique();

            builder.Entity<Attendance>()
    .HasOne(a => a.Student)
    .WithMany()
    .HasForeignKey(a => a.StudentId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attendance>()
                .HasOne(a => a.Class)
                .WithMany()
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ek student ki ek din ki attendance sirf ek baar mark ho sake
            builder.Entity<Attendance>()
                .HasIndex(a => new { a.StudentId, a.AttendanceDate })
                .IsUnique();


            // Class → ClassTeacher (ApplicationUser)
            builder.Entity<Class>()
                .HasOne(c => c.ClassTeacher)
                .WithMany()
                .HasForeignKey(c => c.ClassTeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            // ClassSubject → Teacher (ApplicationUser)
            builder.Entity<ClassSubject>()
                .HasOne(cs => cs.Teacher)
                .WithMany()
                .HasForeignKey(cs => cs.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            // Attendance → MarkedByTeacher (ApplicationUser)
            builder.Entity<Attendance>()
                .HasOne(a => a.MarkedByTeacher)
                .WithMany()
                .HasForeignKey(a => a.MarkedByTeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Exam>()
    .HasOne(e => e.Class)
    .WithMany()
    .HasForeignKey(e => e.ClassId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Marks>()
                .HasOne(m => m.Exam)
                .WithMany(e => e.Marks)
                .HasForeignKey(m => m.ExamId)
                .OnDelete(DeleteBehavior.Cascade);   // Exam delete ho to uske Marks bhi delete ho jayein — ye theek hai

            builder.Entity<Marks>()
                .HasOne(m => m.Student)
                .WithMany()
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Marks>()
                .HasOne(m => m.Subject)
                .WithMany()
                .HasForeignKey(m => m.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ek student ka ek exam me ek subject ka sirf ek hi marks record ho
            builder.Entity<Marks>()
                .HasIndex(m => new { m.ExamId, m.StudentId, m.SubjectId })
                .IsUnique();

            builder.Entity<Fee>()
                .HasOne(f => f.Student)
                .WithMany()
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Notice>()
                .HasOne(n => n.PostedByUser)
                .WithMany()
                .HasForeignKey(n => n.PostedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Teacher>()
    .HasOne(t => t.ApplicationUser)
    .WithOne()
    .HasForeignKey<Teacher>(t => t.ApplicationUserId)
    .OnDelete(DeleteBehavior.SetNull);

        }

    }
}
