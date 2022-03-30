using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectItiTeam.Models.Identity.Repositery
{
    public interface IRepositery 
    {
        string Image { get; set; }
        string Name { get; set; }
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetById(string? id);
        Task<IEnumerable<ApplicationUser>> GetByIDTolist(string? id);
        void LockUser(string UserID);
        void UNLockUser(string UserID);
    }
}
