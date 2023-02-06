using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaddiesPortfolio.Data;
using MaddiesPortfolio.Models;

namespace MaddiesPortfolio.Controllers
{
    public class MProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.MProject.ToListAsync());
        }

        // GET: MProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProject = await _context.MProject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mProject == null)
            {
                return NotFound();
            }

            return View(mProject);
        }

        // GET: MProjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MProjectName,MProjectDescription")] MProject mProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mProject);
        }

        // GET: MProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProject = await _context.MProject.FindAsync(id);
            if (mProject == null)
            {
                return NotFound();
            }
            return View(mProject);
        }

        // POST: MProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MProjectName,MProjectDescription")] MProject mProject)
        {
            if (id != mProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MProjectExists(mProject.Id))
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
            return View(mProject);
        }

        // GET: MProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProject = await _context.MProject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mProject == null)
            {
                return NotFound();
            }

            return View(mProject);
        }

        // POST: MProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mProject = await _context.MProject.FindAsync(id);
            _context.MProject.Remove(mProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MProjectExists(int id)
        {
            return _context.MProject.Any(e => e.Id == id);
        }
    }
}
