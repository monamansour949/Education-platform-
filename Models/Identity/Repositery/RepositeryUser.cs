using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectItiTeam.Models.Identity.Repositery
{
    public class RepositeryUser : IRepositery
    {
        private readonly ApplicationDbContext context;

        public RepositeryUser(ApplicationDbContext context )
        {
            this.context = context;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var data = await context.ApplicationUsers.ToListAsync();
            return data;
        }

        public Task<ApplicationUser> GetById(string? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> GetByIDTolist(string? id)
        {
            var data = await context.ApplicationUsers.Where(x => x.Id != id).ToListAsync();
            return data;
        }
        public void LockUser(string UserID)
        {
            var userfromDB = context.ApplicationUsers.FirstOrDefault(x => x.Id == UserID);
            userfromDB.LockoutEnd = DateTime.Now.AddYears(1000);
            context.SaveChanges();

        }

        public void UNLockUser(string UserID)
        {
            var userfromDB = context.ApplicationUsers.FirstOrDefault(x => x.Id == UserID);
            userfromDB.LockoutEnd = DateTime.Now;
            context.SaveChanges();
        }
    }
}
