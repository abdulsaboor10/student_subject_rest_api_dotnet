using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DbModels
{
    public class StudentDbContext : DbContext
    {
        public DbSet<StudentDbDto> studentDbDto { get; set; }
        public DbSet<StudentSubjectDbDto> studentSubjectDbDto { get; set; }
        public DbSet<SubjectDbDto> subjectDbDto { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentDbDto>().HasKey(e => e.Id);

            // modelBuilder.Entity<StudentDbDto>().HasMany<StudentSubjectDbDto>()
            // .WithOne(sc => sc.studentDbDto).HasForeignKey(s =>  s.StudentId);



            // modelBuilder.Entity<SubjectDbDto>().HasMany<StudentSubjectDbDto>()
            //     .WithOne(sc => sc.SubjectDbDto).HasForeignKey(s  => s.SubjectId);
            modelBuilder.Entity<StudentSubjectDbDto>()
        .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            modelBuilder.Entity<StudentSubjectDbDto>()
                .HasOne(ss => ss.StudentDbDto)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSubjectDbDto>()
                .HasOne(ss => ss.SubjectDbDto)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);

        }

    }
}
