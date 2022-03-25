using ProjectItiTeam.Models;
using System;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface IAudioRepository
    {
        Guid id { get; set; }
        int delete(int id);
        List<Audio> GetAll();
        List<Audio> GetByLevelId(int id);
        Audio GetById(int id);
        int Insert(Audio audio);
        int update(int id, Audio audio);
    }
}
