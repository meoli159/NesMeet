using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NesMeet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NesMeet.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TrainerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var classroomIds = await _context.TraineeClassrooms.Where(t => t.TraineeId == userId).Select(t => t.ClassroomId).ToListAsync();
            var classrooms = await _context.Classrooms.Where(c => classroomIds.Contains(c.Id)).Include(c => c.Course).Include(c => c.Course.Category).ToListAsync();

            return View(classrooms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var classroom = await _context.Classrooms.Include(c => c.ClassProfile).Include(c => c.Course).FirstOrDefaultAsync(m => m.Id == id);
            
            if (classroom == null) return NotFound();
            
            ViewData["Topics"] = await _context.Topics.Include(t => t.Trainer).Where(t => t.ClassroomId == id).ToListAsync();
            ViewData["Trainees"] = await _context.TraineeClassrooms.Include(t => t.Trainee).Where(t => t.ClassroomId == id).ToListAsync();

            return View(classroom);
        }
    }
}
