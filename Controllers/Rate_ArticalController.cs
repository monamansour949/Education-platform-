using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Data.Seed;
using ProjectItiTeam.Models;

namespace ProjectItiTeam.Controllers
{
    public class Rate_ArticalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Rate_ArticalController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize()]
        [HttpGet]
        public async Task<IActionResult> addlike(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var claims = (ClaimsIdentity)User.Identity;
            var clam = claims.FindFirst(ClaimTypes.NameIdentifier);

            Rate_Artical a = await _context.Rate_Articals.Include(x => x.Artical).Where(bb => bb.Artical_ID == id).FirstOrDefaultAsync();
            if (a == null)
            {
                Rate_Artical aa = new Rate_Artical();
                aa.Stars = 1;
                aa.Artical_ID = id;
                aa.UserName = clam.Value;
                _context.Rate_Articals.Add(aa);
                await _context.SaveChangesAsync();
                return Json(a.Stars);

            }
            else
            {
                a.Stars += 1;
                _context.Update(a);
                await _context.SaveChangesAsync();
                return Json(a.Stars);

            }
            return Json("0");
        }


        // GET: Rate_Artical
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rate_Articals.Include(r => r.Artical);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rate_Artical/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate_Artical = await _context.Rate_Articals
                .Include(r => r.Artical)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rate_Artical == null)
            {
                return NotFound();
            }

            return View(rate_Artical);
        }

        // GET: Rate_Artical/Create
        public IActionResult Create()
        {
            ViewData["Artical_ID"] = new SelectList(_context.Articals, "ID", "ID");
            return View();
        }

        // POST: Rate_Artical/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Stars,dislike,Date,Artical_ID,UserName")] Rate_Artical rate_Artical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rate_Artical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Artical_ID"] = new SelectList(_context.Articals, "ID", "ID", rate_Artical.Artical_ID);
            return View(rate_Artical);
        }

        // GET: Rate_Artical/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate_Artical = await _context.Rate_Articals.FindAsync(id);
            if (rate_Artical == null)
            {
                return NotFound();
            }
            ViewData["Artical_ID"] = new SelectList(_context.Articals, "ID", "ID", rate_Artical.Artical_ID);
            return View(rate_Artical);
        }

        // POST: Rate_Artical/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Stars,dislike,Date,Artical_ID,UserName")] Rate_Artical rate_Artical)
        {
            if (id != rate_Artical.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rate_Artical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Rate_ArticalExists(rate_Artical.ID))
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
            ViewData["Artical_ID"] = new SelectList(_context.Articals, "ID", "ID", rate_Artical.Artical_ID);
            return View(rate_Artical);
        }

        // GET: Rate_Artical/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate_Artical = await _context.Rate_Articals
                .Include(r => r.Artical)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rate_Artical == null)
            {
                return NotFound();
            }

            return View(rate_Artical);
        }

        // POST: Rate_Artical/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rate_Artical = await _context.Rate_Articals.FindAsync(id);
            _context.Rate_Articals.Remove(rate_Artical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Rate_ArticalExists(int id)
        {
            return _context.Rate_Articals.Any(e => e.ID == id);
        }
    }
}
