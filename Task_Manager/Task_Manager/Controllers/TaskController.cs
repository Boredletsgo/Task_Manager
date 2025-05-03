using Microsoft.AspNetCore.Mvc;
using Task_Manager.Data;
using Task_Manager.Models;
using System.Linq;

namespace Task_Manager.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        // To-Do List Page
        public IActionResult Todo()
        {
            var tasks = _context.Tasks.ToList(); // Get from DB
            return View(tasks);
        }

        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList(); // Get from DB
            return View(tasks);
        }

        // Show Add Task Form
        public IActionResult Create()
        {
            return View();
        }

        // Handle Add Task Form Post
        [HttpPost]
        public IActionResult Create(TaskItem newTask)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(newTask);
                _context.SaveChanges(); // Save to DB
                return RedirectToAction("Index");
            }
            return View(newTask);
        }

        // Show Update Task Form
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // Handle Update Task Form Post
        [HttpPost]
        public IActionResult Edit(TaskItem updatedTask)
        {
            if (ModelState.IsValid)
            {
                var task = _context.Tasks.Find(updatedTask.Id);
                if (task != null)
                {
                    task.Title = updatedTask.Title;
                    task.Description = updatedTask.Description;
                    task.IsCompleted = updatedTask.IsCompleted;
                    _context.SaveChanges(); // Save updates to DB
                }
                return RedirectToAction("Index");
            }
            return View(updatedTask);
        }
    }
}
