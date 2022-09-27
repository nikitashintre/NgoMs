using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ngo.Data;
using Ngo.Models;

namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize]
    public class NgoEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NgoEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events/NgoEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.NgoEvents.ToListAsync());
        }

        // GET: Events/NgoEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngoEvent = await _context.NgoEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (ngoEvent == null)
            {
                return NotFound();
            }

            return View(ngoEvent);
        }

        // GET: Events/NgoEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/NgoEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,Discription,StartDate,EndDate")] NgoEvent ngoEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ngoEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ngoEvent);
        }

        // GET: Events/NgoEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngoEvent = await _context.NgoEvents.FindAsync(id);
            if (ngoEvent == null)
            {
                return NotFound();
            }
            return View(ngoEvent);
        }

        // POST: Events/NgoEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,Discription,StartDate,EndDate")] NgoEvent ngoEvent)
        {
            if (id != ngoEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ngoEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NgoEventExists(ngoEvent.EventId))
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
            return View(ngoEvent);
        }

        // GET: Events/NgoEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngoEvent = await _context.NgoEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (ngoEvent == null)
            {
                return NotFound();
            }

            return View(ngoEvent);
        }

        // POST: Events/NgoEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ngoEvent = await _context.NgoEvents.FindAsync(id);
            _context.NgoEvents.Remove(ngoEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NgoEventExists(int id)
        {
            return _context.NgoEvents.Any(e => e.EventId == id);
        }
    }
}
