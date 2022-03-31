using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System;
using System.Linq;

namespace ProjectItiTeam.Hubs
{
    public class Like_Artical_Hub: Hub
    {
        private readonly ApplicationDbContext context;

        public Like_Artical_Hub(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void SendMEssageFromClient(string input, string name, string ArticalID)
        {
            //check exist here or no 
            Rate_Artical model = new Rate_Artical();
            model.UserName = name;
            model.Date = DateTime.Now;
            int article = int.Parse(ArticalID);
            Rate_Artical Model = context.Rate_Articals.Include(x => x.Artical).FirstOrDefault(x => x.Artical_ID == article);


            if (input == "1")
            {
                if (Model == null)
                {
                    Rate_Artical r = new Rate_Artical();
                    r.Stars = 1;
                    r.Artical_ID = int.Parse(ArticalID);
                    r.Date = DateTime.Now;
                    context.Rate_Articals.Add(r);
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
                    Rate_Artical r = new Rate_Artical();
                    r.dislike = 1;
                    r.Artical_ID = int.Parse(ArticalID);
                    r.Date = DateTime.Now;
                    context.Rate_Articals.Add(r);
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
            Rate_Artical data = context.Rate_Articals.Include(x => x.Artical).Where(x => x.Artical_ID == article).FirstOrDefault();
            if (input == "1")
            {
                Clients.All.SendAsync("Display", data.Artical_ID, data.Stars, "1");//notifuication "Push"
            }
            else
            {
                Clients.All.SendAsync("Display", data.Artical_ID, data.dislike, "0");//notifuication "Push"
            }
        }
      }
 }
