using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ngo.Data;
using Ngo.Models;



namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize]
    public class CampaignsController : Controller
    {
        private readonly ApplicationDbContext _context;                 //context to database
        
        public CampaignsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events/Campaigns
        public async Task<IActionResult> Index()
        {
              
            var applicationDbContext = _context.Campaigns.Include(c => c.CampaignCategory);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: LibMgmt/GetCampaignsOfCategory?filterCategoryId=5
        public async Task<IActionResult> GetCampaignsOfCategory(int filterCategoryId)
        {
            var viewmodel = await _context.Campaigns
                .Where(b => b.CategoryId == filterCategoryId)
                                          .Include(b => b.CampaignCategory)
                                          .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }
       

        // GET: Events/Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //DonationI donationI = new DonationI();

            ////total = 0;
            //for (int i = 1; i <= _context.DonationIs.Count(); i++)
            //{
            //    if (donationI.CampaignId == id)
            //    {
            //        total = donationI.DonationAmount + total;
            //    }
            //}
            //ViewBag.Total = total;

            var campaign = await _context.Campaigns
                .Include(c => c.CampaignCategory)
                .FirstOrDefaultAsync(m => m.CamaignId == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Events/Campaigns/Create
        [Authorize(Roles = "NgoAdmin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.CampaignCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Events/Campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "NgoAdmin")]
        public async Task<IActionResult> Create([Bind("CamaignId,CampaignName,Discription,ImageUrl,CategoryId")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CampaignCategories, "CategoryId", "CategoryName", campaign.CategoryId);
            return View(campaign);
        }

        // GET: Events/Campaigns/Edit/5
        [Authorize(Roles = "NgoAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.CampaignCategories, "CategoryId", "CategoryName", campaign.CategoryId);
            return View(campaign);
        }

        // POST: Events/Campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "NgoAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("CamaignId,CampaignName,Discription,ImageUrl,CategoryId")] Campaign campaign)
        {
            if (id != campaign.CamaignId)
            {
                return NotFound();
            }
            // Sanitize the data
            campaign.CampaignName = campaign.CampaignName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Campaigns
                .Any(c => c.CampaignName == campaign.CampaignName && c.CamaignId != campaign.CamaignId);
            
            if (duplicateExists)
            {
                ModelState.AddModelError("CampaignName", "Duplicate Campaign Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.CamaignId))
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
            ViewData["CategoryId"] = new SelectList(_context.CampaignCategories, "CategoryId", "CategoryName", campaign.CategoryId);
            return View(campaign);
        }

        // GET: Events/Campaigns/Delete/5
        [Authorize(Roles = "NgoAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.CampaignCategory)
                .FirstOrDefaultAsync(m => m.CamaignId == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Events/Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "NgoAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.CamaignId == id);
        }
    }
}
