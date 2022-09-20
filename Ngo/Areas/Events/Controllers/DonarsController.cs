using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using Ngo.Data;
using Ngo.Models;

namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    public class DonarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events/Donars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Donars.Include(d => d.Event);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Events/Donars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donar = await _context.Donars
                .Include(d => d.Event)
                .FirstOrDefaultAsync(m => m.DonarId == id);
            if (donar == null)
            {
                return NotFound();
            }

            return View(donar);
        }

        // GET: Events/Donars/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
            return View();
        }
        //public IActionResult Create(int id)
        //{

        //    var query = _context.Events.Where(e => e.EventId == id);
        //    ViewBag.EventId = query;

        //    //ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
        //    return View();
        //}

        // POST: Events/Donars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonarId,DonarName,Email,Mobile,EventId")] Donar donar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", donar.EventId);
            return View(donar);
        }

        // GET: Events/Donars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donar = await _context.Donars.FindAsync(id);
            if (donar == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", donar.EventId);
            return View(donar);
        }

        // POST: Events/Donars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonarId,DonarName,Email,Mobile,EventId")] Donar donar)
        {
            if (id != donar.DonarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonarExists(donar.DonarId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", donar.EventId);
            return View(donar);
        }

        // GET: Events/Donars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donar = await _context.Donars
                .Include(d => d.Event)
                .FirstOrDefaultAsync(m => m.DonarId == id);
            if (donar == null)
            {
                return NotFound();
            }

            return View(donar);
        }

        // POST: Events/Donars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donar = await _context.Donars.FindAsync(id);
            _context.Donars.Remove(donar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonarExists(int id)
        {
            return _context.Donars.Any(e => e.DonarId == id);
        }
    }
}
