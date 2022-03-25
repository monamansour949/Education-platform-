using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class basketRepo : IBAox
    {
        private readonly ApplicationDbContext context;

        public basketRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
           var data= GetById(id);
           context.Tables.Remove(data);
            context.SaveChanges();
        }

        public List<Table> GetAll()
        {
            return context.Tables.Include(ws => ws.ApplicationUsers).Include(ww => ww.Courses).ToList();
        }
        public Table GetById(int id)
        {
            return context.Tables.Find(id);
        }
        public void Insert(Table newCourse)
        {
            context.Add(newCourse);
            context.SaveChanges();
        }
        public void Update(int id, Table Table)
        {
            var data = GetById(id);
            context.Update(data);
        }
    }
}
