using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ngo.Areas.Events.ViewModels;
using Ngo.Data;
using Ngo.Models;

namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    [Authorize]
    public class DonationIsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationIsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events/DonationIs
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.DonationIs.Include(d => d.Campaign);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LibMgmt/GetDonorsOfCategory?filterCategoryId=5
        public async Task<IActionResult> GetDonorsOfCategory(int filterCategoryId)
        {
            var viewmodel = await _context.DonationIs
                                          .Where(b => b.CampaignId == filterCategoryId)
                                          .Include(b => b.Campaign)
                                          .ToListAsync();
            //var total = 0;
            //total = await _context.DonationIs.Where(d => d.CampaignId == filterCategoryId).Include(b => b.Campaign).SumAsync(d => d.DonationAmount);
            //ViewBag.DonationAmount = total;
            return View(viewName: "Index", model: viewmodel);
        }

        // GET: Events/DonationIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationI = await _context.DonationIs
                .Include(d => d.Campaign)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (donationI == null)
            {
                return NotFound();
            }

            return View(donationI);
        }

        // GET: Events/DonationIs/Create
        public IActionResult Create1()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("DonationId,DonarName,Email,Mobile,DonationAmount,CampaignId")] DonationI donationI)
        {

            if (ModelState.IsValid)
            {
                _context.Add(donationI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", donationI.CampaignId);
            return View(donationI);
        }
        public IActionResult Create(int id)
        {
            //DonationI donation = new DonationI();
           
                var Camp = _context.Campaigns.SingleOrDefault(c => c.CamaignId == id);
                ViewBag.CampaignId = Camp.CamaignId;
                ViewBag.CampaignName = Camp.CampaignName;

                //ViewData["CampaignId"] = new SelectList(Camp, "CampaignId", "CampaignName");
            //ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName");
            return View();
        }

        // POST: Events/DonationIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationId,DonarName,Email,Mobile,DonationAmount,CampaignId")] DonationI donationI)
        {

            if (ModelState.IsValid)
            {
                _context.Add(donationI);
                //ShowCampaignsViewModel showCampaignsViewModel = new ShowCampaignsViewModel();
                //for (int i = 0; i < _context.Campaigns.Count(); i++)
                //{
                //    if (donationI.CampaignId == i)
                //    {
                //        showCampaignsViewModel.Total = donationI.DonationAmount + showCampaignsViewModel.Total;
                //    }
                //}
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", donationI.CampaignId);
            return View(donationI);
        }

        // GET: Events/DonationIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationI = await _context.DonationIs.FindAsync(id);
            if (donationI == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", donationI.CampaignId);
            return View(donationI);
        }

        // POST: Events/DonationIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationId,DonarName,Email,Mobile,DonationAmount,CampaignId")] DonationI donationI)
        {
            if (id != donationI.DonationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationIExists(donationI.DonationId))
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
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "CamaignId", "CampaignName", donationI.CampaignId);
            return View(donationI);
        }

        // GET: Events/DonationIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationI = await _context.DonationIs
                .Include(d => d.Campaign)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (donationI == null)
            {
                return NotFound();
            }

            return View(donationI);
        }

        // POST: Events/DonationIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donationI = await _context.DonationIs.FindAsync(id);
            _context.DonationIs.Remove(donationI);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationIExists(int id)
        {
            return _context.DonationIs.Any(e => e.DonationId == id);
        }
    }
}
