using ProjectItiTeam.Models;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface ILevelRepository
    {
        int Delete(int id);
        List<Level> GetAll();
        Level GetById(int id);
        int Insert(Level newlevel);
        int Update(int id, Level level);
    }
}