using ProjectItiTeam.Models;
using System.Linq;

using ProjectItiTeam.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace ProjectItiTeam.Repository
{
    public class TrueAndFalseRepository : ITrueAndFalseRepository
    {
        ApplicationDbContext Context;
        public TrueAndFalseRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public List<TrueAndFalse> GetAllByLevelId(int id)
        {
            return Context.TrueAndFalses.Where(TF => TF.Level_Id == id).ToList();
        }

        public TrueAndFalse GetById(int id)
        {
            return Context.TrueAndFalses.FirstOrDefault(TF => TF.Id == id);
        }

        public int Insert(TrueAndFalse trueAndFalse)
        {
            Context.TrueAndFalses.Add(trueAndFalse);
            return Context.SaveChanges();
        }

        public int Update(int id, TrueAndFalse trueAndFalse)
        {
            TrueAndFalse trueAndFalseUpdated = GetById(id);
            if (trueAndFalseUpdated != null)
            {
                trueAndFalseUpdated.Id = trueAndFalse.Id;
                trueAndFalseUpdated.Text = trueAndFalse.Text;
                trueAndFalseUpdated.Answer = trueAndFalse.Answer;

                return Context.SaveChanges();

            }
            return 0;
        }

        public int Delete(int id)
        {
            TrueAndFalse trueAndFalse = GetById(id);
            Context.TrueAndFalses.Remove(trueAndFalse);
            return Context.SaveChanges();
        }
    }
}
