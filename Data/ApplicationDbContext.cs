using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity;
using ProjectItiTeam.Models.ViewModel;
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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public DbSet<Exam> Exams { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Level> levels { get; set; }
    }
}
