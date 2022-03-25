using ProjectItiTeam.Models;
using System;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface IVideoRepository
    {
        Guid id { get; set; }
        int delete(int id);
        List<Video> GetAll();
        List<Video> GetByCourseId(int id);
        Video GetById(int id);
        int Insert(Video video);
        int update(int id, Video video);
    }
}
