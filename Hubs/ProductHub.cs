using Microsoft.AspNetCore.SignalR;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Repository;
using System;
using System.Security.Claims;

namespace ProjectItiTeam.Hubs
{
    public class ProductHub: Hub
    {
        private readonly IRateRepository rateRepo;
        private readonly IRepositery repositery;

        public ProductHub(IRateRepository rateRepo, IRepositery repositery)
        {
            this.rateRepo = rateRepo;
            this.repositery = repositery;
        }
        public void SendMEssageFromClient(string star, string Message, string idCourse, string user)
        {
           // var clamidentity = (ClaimsIdentity)this.User.Identity;
            //var clams = clamidentity.FindFirst(ClaimTypes.NameIdentifier);

            //var data =   repositery.GetById(clams.Value);
            //logic C#
            Rate model = new Rate()
            {
                Stars = int.Parse(star),
                Date= DateTime.Now,
               // UserName=data.Name,
                Course_ID= int.Parse(idCourse)
            };

            rateRepo.Insert(model);
           // Clients.All.SendAsync("Display", username, Message, idproduct);//notifuication "Push"
        }
    }
}
