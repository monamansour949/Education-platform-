using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class VideoRepository : IVideoRepository
    {
        ApplicationDbContext context;//= new ApplicationDbContext();
        public VideoRepository(ApplicationDbContext _Context)
        {
            context = _Context;
        }

        public VideoRepository()
        {
            id = Guid.NewGuid();
        }
        public Guid id { get; set; }

        public List<Video> GetAll()
        {
            return context.Videos.Include(v => v.Course).ToList();
        }
        public Video GetById(int id)
        {
            return context.Videos.Include(v => v.Course).FirstOrDefault(v => v.ID == id);
        }
        public int Insert(Video video)
        {
            context.Videos.Add(video);
            return context.SaveChanges();
        }
        public int update(int id, Video _video)
        {
            Video video = context.Videos.Include(v => v.Course).FirstOrDefault(v => v.ID == id);
            if (video != null)
            {
                video.Name = _video.Name;
                video.Description = _video.Description;
                video.Course_ID = _video.Course_ID;
                video.User_Id = _video.User_Id;
                return context.SaveChanges();
            }
            return 0;
        }
        public int delete(int id)
        {
            context.Videos.Remove(GetById(id));
            return context.SaveChanges();
        }
        public List<Video> GetByCourseId(int id)
        {
            List<Video> video = context.Videos.Where(v => v.Course_ID == id).ToList();
            return video;
        }

    }
}