using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class AudioRepository : IAudioRepository
    {
        ApplicationDbContext context;//= new ApplicationDbContext();
        public AudioRepository(ApplicationDbContext _Context)
        {
            context = _Context;
        }

        public AudioRepository()
        {
            id = Guid.NewGuid();
        }
        public Guid id { get; set; }

        public List<Audio> GetAll()
        {
            return context.Audios.Include(a => a.Level).ToList();
        }
        public Audio GetById(int id)
        {
            return context.Audios.Include(a => a.Level).FirstOrDefault(a => a.ID == id);
        }
        public int Insert(Audio audio)
        {
            context.Audios.Add(audio);
            return context.SaveChanges();
        }
        public int update(int id, Audio _audio)
        {
            Audio audio = context.Audios.Include(a => a.Level).FirstOrDefault(a => a.ID == id);
            if (audio != null)
            {
                audio.Name = _audio.Name;
                audio.Description = _audio.Description;
                audio.Level_ID = _audio.Level_ID;
                audio.User_Id = _audio.User_Id;
                return context.SaveChanges();
            }
            return 0;
        }
        public int delete(int id)
        {
            context.Audios.Remove(GetById(id));
            return context.SaveChanges();
        }
        public List<Audio> GetByDeptId(int id)
        {
            List<Audio> Crs = context.Audios.Where(c => c.Level_ID == id).ToList();
            return Crs;
        }

        public List<Audio> GetByLevelId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
