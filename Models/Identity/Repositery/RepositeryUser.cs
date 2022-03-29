﻿using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectItiTeam.Models.Identity.Repositery
{
    public class RepositeryUser : IRepositery
    {
        private readonly ApplicationDbContext context;
        public string Image { get; set; }
        public string Name { get; set; }
        public RepositeryUser(ApplicationDbContext context )
        {
            this.context = context;

          
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var data = await context.ApplicationUsers.ToListAsync();
            return data;
        }
        public async Task<ApplicationUser> GetById(string? id)
        {
            ApplicationUser data = await context.ApplicationUsers.FirstOrDefaultAsync(x=>x.Id==id);
            return data;
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
