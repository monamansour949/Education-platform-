using ProjectItiTeam.Models;
using System;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface IRateRepository
    {
        Guid id { get; set; }
        int delete(int id);
        List<Rate> GetAll();
        List<Rate> GetByCourseId(int id);
        Rate GetById(int id);
        int Insert(Rate rate);
        int update(int id, Rate rate);
    }
}
