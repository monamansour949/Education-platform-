using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;

namespace ProjectItiTeam.Controllers
{
    public class ArticalsController : Controller
    {
        private readonly IRepositery repositery;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly ICourseRepository courseRepository;
        private readonly ILevelRepository levelRepository;

        public ArticalsController(IRepositery repositery,IWebHostEnvironment WebHostEnvironment,ApplicationDbContext context, ICourseRepository courseRepository, ILevelRepository levelRepository)
        {
            this.repositery = repositery;
            webHostEnvironment = WebHostEnvironment;
            _context = context;
            this.courseRepository = courseRepository;
            this.levelRepository = levelRepository;
        }

        // GET: Articals
        public async Task<IActionResult> Index()
        {
            var data = await _context.Articals.ToListAsync();
            ViewBag.dataa =  _context.Rate_Articals.Include(x=>x.Artical).ToList();
            return View(data);
        }
        public async Task<IActionResult> IndexArticle()
        {
            ModelViewIndex mode = new ModelViewIndex();

            mode.ArticalLi = await _context.Articals.ToListAsync();
            mode.coursesLi = courseRepository.GetAll();
            mode.LevelLi = levelRepository.GetAll();
            return View(mode);
        }

        // GET: Articals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artical = await _context.Articals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artical == null)
            {
                return NotFound();
            }

            return View(artical);
        }

        // GET: Articals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserNAme,Image,title,body,date")] Artical artical)
        {
            if (ModelState.IsValid)
            {
                string VidPAth = @"\PhotoArticle\default.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "PhotoArticle", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    VidPAth = @"\PhotoArticle\" + d + ImageName;
                }
                artical.Image = VidPAth;

                var clamidentity = (ClaimsIdentity)this.User.Identity;
                var clams = clamidentity.FindFirst(ClaimTypes.NameIdentifier);

                var data = await repositery.GetById(clams.Value);
                artical.UserNAme = data.Name;
                artical.date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                _context.Add(artical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artical);
        }

        // GET: Articals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artical = await _context.Articals.FindAsync(id);
            if (artical == null)
            {
                return NotFound();
            }
            return View(artical);
        }

        // POST: Articals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserNAme,Image,title,body,date")] Artical artical)
        {
            if (ModelState.IsValid)
            {
                string VidPAth = @"\PhotoArticle\default.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "PhotoArticle", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    VidPAth = @"\PhotoArticle\" + d + ImageName;
                }
                artical.Image = VidPAth;
            }
            if (id != artical.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {



                    _context.Update(artical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticalExists(artical.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artical);
        }

        // GET: Articals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artical = await _context.Articals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artical == null)
            {
                return NotFound();
            }

            return View(artical);
        }

        // POST: Articals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artical = await _context.Articals.FindAsync(id);
            _context.Articals.Remove(artical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticalExists(int id)
        {
            return _context.Articals.Any(e => e.ID == id);
        }
    }
}
