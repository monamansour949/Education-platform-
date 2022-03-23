using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System.Collections.Generic;
using System.Linq;



namespace ProjectItiTeam.Repository
{
    public class LevelRepository : ILevelRepository
    {
        ApplicationDbContext context;

        public LevelRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Level> GetAll()
        {
            return context.levels.ToList();
        }

        public Level GetById(int id)
        {
            return context.levels.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Level newlevel)
        {
            context.levels.Add(newlevel);
            return context.SaveChanges();
        }

        public int Update(int id, Level level)
        {
            Level oldlevel = GetById(id);
            if (oldlevel != null)
            {
                oldlevel.Name = level.Name;


                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Level oldlevel = GetById(id);

            context.levels.Remove(oldlevel);
            return context.SaveChanges();
        }

    }
}
