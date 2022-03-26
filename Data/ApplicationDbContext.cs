using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectItiTeam.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        { 
            base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public DbSet<Exam> Exams { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Level> levels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<MCQ> MCQs { get; set; }
        public DbSet<choice> Choices { get; set; }
        public DbSet<TrueAndFalse> TrueAndFalses { get; set; }
        public DbSet<Quiz> Quizzes  { get; set; }




    }
}
