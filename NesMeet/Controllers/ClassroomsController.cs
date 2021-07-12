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
    public class ClassroomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Classrooms.Include(c => c.ClassProfile).Include(c => c.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.ClassProfile)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classroom == null)
            {
                return NotFound();
            }

            ViewData["Topics"] = await _context.Topics.Include(t => t.Trainer).Where(t => t.ClassroomId == id).ToListAsync();
            ViewData["Trainees"] = await _context.TraineeClassrooms.Include(t => t.Trainee).Where(t => t.ClassroomId == id).ToListAsync();
            return View(classroom);
        }

        // GET: Classrooms/Create
        public IActionResult Create()
        {
            ViewData["ClassProfileId"] = new SelectList(_context.ClassProfiles, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                classroom.Year = classroom.StartDate.Year;
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassProfileId"] = new SelectList(_context.ClassProfiles, "Id", "Id", classroom.ClassProfileId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", classroom.CourseId);
            return View(classroom);
        }

        // GET: Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            ViewData["ClassProfileId"] = new SelectList(_context.ClassProfiles, "Id", "Id", classroom.ClassProfileId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", classroom.CourseId);
            return View(classroom);
        }

        // POST: Classrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    classroom.Year = classroom.StartDate.Year;
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.Id))
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
            ViewData["ClassProfileId"] = new SelectList(_context.ClassProfiles, "Id", "Id", classroom.ClassProfileId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", classroom.CourseId);
            return View(classroom);
        }

        // GET: Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.ClassProfile)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomExists(int id)
        {
            return _context.Classrooms.Any(e => e.Id == id);
        }
        
        public async Task<IActionResult> AddToClassroom(int? classroomId)
        {
            if (classroomId == null) return NotFound();
            var classroom = await _context.Classrooms.FindAsync(classroomId);
            if (classroom == null) return NotFound();

            var traineeIds = await _context.UserRoles.Where(u => u.RoleId == "Trainee").Select(u => u.UserId).ToListAsync();
            var trainees = await _context.Users.Where(u => traineeIds.Contains(u.Id)).ToListAsync();

            var currentTraineeIds = await _context.TraineeClassrooms.Where(t => t.ClassroomId == classroomId).Select(t => t.TraineeId).ToListAsync();
            var currentTrainees = trainees.Where(t => currentTraineeIds.Contains(t.Id)).ToList();
            var remainingTrainees = trainees.Where(t => !currentTraineeIds.Contains(t.Id)).ToList();

            ViewData["RemainingTrainees"] = remainingTrainees;
            ViewData["CurrentTrainees"] = currentTrainees;

            return View(classroom);
        }

         [HttpPost]
         [AutoValidateAntiforgeryToken]
         public async Task<IActionResult> AddTrainee(int? classroomId, string traineeId)
         {
            if(classroomId != null && traineeId!=null)
            {
                var traineeClassroom = new TraineeClassroom()
                {
                    ClassroomId = (int)classroomId,
                    TraineeId = traineeId
                };
                _context.Add(traineeClassroom);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AddToClassroom), new { id = classroomId });
         }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveTrainee(int? classroomId, string traineeId)
        {
            var traineeClassroom = await _context.TraineeClassrooms.FirstOrDefaultAsync(t => t.ClassroomId == classroomId && t.TraineeId == traineeId);
                if (traineeClassroom !=null)
            {
                _context.Add(traineeClassroom);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(AddToClassroom), new { id = classroomId });
        }
    }
}
