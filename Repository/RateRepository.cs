using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class RateRepository : IRateRepository
    {
        ApplicationDbContext context;//= new ApplicationDbContext();
        public RateRepository(ApplicationDbContext _Context)
        {
            context = _Context;
        }

        public RateRepository()
        {
            id = Guid.NewGuid();
        }
        public Guid id { get; set; }

        public List<Rate> GetAll()
        {
            return context.Rates.Include(r => r.Course).ToList();
        }
        public Rate GetById(int id)
        {
            return context.Rates.Include(r => r.Course).FirstOrDefault(c => c.ID == id);
        }
        public int Insert(Rate rate)
        {
            context.Rates.Add(rate);
            return context.SaveChanges();
        }
        public int update(int id, Rate _rate)
        {
            Rate rate = context.Rates.Include(r => r.Course).FirstOrDefault(r => r.ID == id);
            if (rate != null)
            {
                rate.Stars = _rate.Stars;
                rate.Course_ID = _rate.Course_ID;
                rate.User_ID = _rate.User_ID;
                return context.SaveChanges();
            }
            return 0;
        }
        public int delete(int id)
        {
            context.Rates.Remove(GetById(id));
            return context.SaveChanges();
        }
        public List<Rate> GetByCourseId(int id)
        {
            List<Rate> rate = context.Rates.Where(r => r.Course_ID == id).ToList();
            return rate;
        }
    }
}
