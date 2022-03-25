using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Models;

namespace ProjectItiTeam.Data
{
    public class ProjectItiTeamContext : DbContext
    {
        public ProjectItiTeamContext (DbContextOptions<ProjectItiTeamContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectItiTeam.Models.Course> Course { get; set; }

        public DbSet<ProjectItiTeam.Models.Exam> Exam { get; set; }

        public DbSet<ProjectItiTeam.Models.Level> Level { get; set; }
    }
}
