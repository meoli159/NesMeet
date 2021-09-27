using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NesMeet.Data;
using NesMeet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NesMeet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
            var classrooms = await _context.Classrooms.Include(c => c.ClassProfile).Include(c => c.Course).ToListAsync();
            return View(classrooms);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.Include(c => c.ClassProfile).Include(c => c.Course).FirstOrDefaultAsync(m => m.Id == id);
            if (classroom == null)
            {
                return NotFound();
            }

            ViewData["Topics"] = await _context.Topics.Include(t => t.Trainer).Where(t => t.ClassroomId == id).ToListAsync();
            ViewData["Trainees"] = await _context.TraineeClassrooms.Include(t => t.Trainee).Where(t => t.ClassroomId == id).ToListAsync();
            return View(classroom);
        }
    }
}
