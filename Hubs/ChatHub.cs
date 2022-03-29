using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ApplicationDbContext context;

        public ChatHub(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void SendMEssageFromClient(string input,string name,string CourseID)
        {
            //check exist here or no 
            Rate model = new Rate();
            model.UserName = name;
            model.Date = DateTime.Now;
            int course = int.Parse(CourseID);

            Rate Model = context.Rates.Include(x => x.Course).FirstOrDefault(x => x.Course_ID == course);
            if (input == "1")
            {
                if (Model ==null)
                {
                    Rate r = new Rate();
                    r.Stars = 1;
                    r.Course_ID = int.Parse(CourseID);
                    r.Date = DateTime.Now;
                    context.Rates.Add(r);
                    context.SaveChanges();
                }
                else
                {
                    Model.Stars++;
                    context.Update(Model);
                    context.SaveChanges();
                }
            }
            else
            {
                if (Model == null)
                {
                    Rate r = new Rate();
                    r.dislike=1;
                    r.Course_ID = int.Parse(CourseID);
                    r.Date = DateTime.Now;
                    context.Rates.Add(r);
                    context.SaveChanges();
                }
                else
                {
                    Model.dislike++;
                    context.Update(Model);
                    context.SaveChanges();
                }
            }

            //ConnectionID[RevicerNAme]
            var data = context.Rates.Include(x => x.Course).Where(x => x.Course_ID == course).FirstOrDefault();
            if (input =="1")
            {
                Clients.All.SendAsync("Display", data.Course_ID,data.Stars,"1");//notifuication "Push"
            }
            else
            {
                Clients.All.SendAsync("Display", data.Course_ID,data.dislike,"0");//notifuication "Push"
            }
        }



        public void SendMEssageAddFav(string name , string CourseID)
        {
            int id_course = int.Parse(CourseID);
            var data = context.Tables.FirstOrDefault(x => x.ApplicationUserID == name && x.Course == id_course);
            if (data == null)
            {
                Table model = new Table()
                {
                    Course = id_course,
                    ApplicationUserID = name,
                    Date = DateTime.Now
                };
                context.Tables.Add(model);
                context.SaveChanges();
                Clients.All.SendAsync("Display_data", name,"success add data");//notifuication "Push"
            }
            else
            {
                string datasend = "sorry this course exist";
                Clients.All.SendAsync("Display_data",name, datasend);//notifuication "Push"
            }

        }
    }
}
