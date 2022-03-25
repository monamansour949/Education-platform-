using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectItiTeam.Components
{
    [ViewComponent(Name = "categoryMune")]
    public class categoryMune : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
