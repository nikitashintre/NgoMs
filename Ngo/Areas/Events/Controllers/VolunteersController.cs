using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ngo.Data;
using Ngo.Models;
using Ngo.Models.Enums;
using Microsoft.AspNetCore.Identity;




namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize]
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VolunteersController> _logger;

        public VolunteersController(ApplicationDbContext context, ILogger<VolunteersController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        // GET: Events/Volunteers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Volunteers.Include(v => v.Campaign);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: LibMgmt/GetVolunteersOfCategory?filterCategoryId=5
        public async Task<IActionResult> GetVolunteersOfCategory(int filterCategoryId)
        {
            var viewmodel = await _context.Volunteers
                                          .Where(b => b.CampaignId == filterCategoryId)
                                          .Include(b => b.Campaign)
                                          .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }
        // GET: Events/Volunteers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.Campaign)
                .FirstOrDefaultAsync(m => m.VolunteerId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: Events/Volunteers/Create
        //[Authorize(Roles = "NgoMember")]
        public IActionResult Create()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName");
            return View();
        }

        // POST: Events/Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "NgoMember")]
        public async Task<IActionResult> Create([Bind("VolunteerId,VolunteerName,Email,Mobile,City,CampaignId")] Volunteer volunteer)
        {
            // Sanitize the data
            volunteer.Email = volunteer.Email.Trim();

            // Validation Checks - Server-side validation
           
            bool duplicateExists = _context.Volunteers.Any(v => v.Email == volunteer.Email);
            if (duplicateExists)
            {
                ModelState.AddModelError("Email", "You Have already joined us as Volunteer! To Change the Campaign volunteering ask Admin");
            }

            if (ModelState.IsValid)
            {
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created a New Category: ID = {volunteer.VolunteerId} !");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", volunteer.CampaignId);
            return View(volunteer);
        }

        // GET: Events/Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", volunteer.CampaignId);
            return View(volunteer);
        }

        // POST: Events/Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VolunteerId,VolunteerName,Email,Mobile,City,CampaignId")] Volunteer volunteer)
        {
            if (id != volunteer.VolunteerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteer.VolunteerId))
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
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", volunteer.CampaignId);
            return View(volunteer);
        }

        // GET: Events/Volunteers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.Campaign)
                .FirstOrDefaultAsync(m => m.VolunteerId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Events/Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.VolunteerId == id);
        }
    }
}
