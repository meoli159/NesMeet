using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NesMeet.Data;
using NesMeet.Models;

namespace NesMeet.Controllers
{
    public class ClassProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClassProfiles.ToListAsync());
        }

        // GET: ClassProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classProfile = await _context.ClassProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classProfile == null)
            {
                return NotFound();
            }

            return View(classProfile);
        }

        // GET: ClassProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,EnrolmentYear,PreferredCampus")] ClassProfile classProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classProfile);
        }

        // GET: ClassProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classProfile = await _context.ClassProfiles.FindAsync(id);
            if (classProfile == null)
            {
                return NotFound();
            }
            return View(classProfile);
        }

        // POST: ClassProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Code,EnrolmentYear,PreferredCampus")] ClassProfile classProfile)
        {
            if (id != classProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassProfileExists(classProfile.Id))
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
            return View(classProfile);
        }

        // GET: ClassProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classProfile = await _context.ClassProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classProfile == null)
            {
                return NotFound();
            }

            return View(classProfile);
        }

        // POST: ClassProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var classProfile = await _context.ClassProfiles.FindAsync(id);
            _context.ClassProfiles.Remove(classProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassProfileExists(int? id)
        {
            return _context.ClassProfiles.Any(e => e.Id == id);
        }
    }
}
