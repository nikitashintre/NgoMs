using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ngo.Data;
using Ngo.Models;

namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize]         //Check if user has Logged in
    public class CampaignCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;    //Added context to database
        private readonly ILogger<CampaignCategory> _logger;   //_logger entry

        public CampaignCategoriesController(ApplicationDbContext context, ILogger<CampaignCategory> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Events/CampaignCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CampaignCategories.ToListAsync());
        }

        // GET: Events/CampaignCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignCategory = await _context.CampaignCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (campaignCategory == null)
            {
                return NotFound();
            }

            return View(campaignCategory);
        }

        // GET: Events/CampaignCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/CampaignCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] CampaignCategory campaignCategory)
        {
            // Sanitize the data
            campaignCategory.CategoryName = campaignCategory.CategoryName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.CampaignCategories.Any(c => c.CategoryName ==campaignCategory.CategoryName);
            if (duplicateExists)            //Check for duplicity
            {
                ModelState.AddModelError("CategoryName", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                _context.Add(campaignCategory);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created a New Category: ID = {campaignCategory.CategoryId} !");
                return RedirectToAction(nameof(Index));
            }
            return View(campaignCategory);
        }

        // GET: Events/CampaignCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignCategory = await _context.CampaignCategories.FindAsync(id);
            if (campaignCategory == null)
            {
                return NotFound();
            }
            return View(campaignCategory);
        }

        // POST: Events/CampaignCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] CampaignCategory campaignCategory)
        {
            if (id != campaignCategory.CategoryId)
            {
                return NotFound();
            }

            // Sanitize the data
            campaignCategory.CategoryName = campaignCategory.CategoryName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.CampaignCategories
                .Any(c => c.CategoryName == campaignCategory.CategoryName && c.CategoryId != campaignCategory.CategoryId);
            if (duplicateExists)
            {
                ModelState.AddModelError("CategoryName", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaignCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignCategoryExists(campaignCategory.CategoryId))
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
            return View(campaignCategory);
        }

        // GET: Events/CampaignCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignCategory = await _context.CampaignCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (campaignCategory == null)
            {
                return NotFound();
            }

            return View(campaignCategory);
        }

        // POST: Events/CampaignCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaignCategory = await _context.CampaignCategories.FindAsync(id);
            _context.CampaignCategories.Remove(campaignCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignCategoryExists(int id)
        {
            return _context.CampaignCategories.Any(e => e.CategoryId == id);
        }
    }
}
