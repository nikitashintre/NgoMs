using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Ngo.Areas.Events.ViewModels;
using Ngo.Data;
using Ngo.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ngo.Areas.Events.Controllers
{
    public class ShowCampaignsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ShowCampaignsController> _logger;

        public ShowCampaignsController(
            ApplicationDbContext dbContext,
            ILogger<ShowCampaignsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            PopulateDropDownListToSelectCategory();

            _logger.LogInformation("--- extracted Categories");
            

            return View();
        }
        private void PopulateDropDownListToSelectCategory()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem
            {
                Text = "----- select a category -----",
                Value = "",
                Selected = true
            });
            categories.AddRange(new SelectList(_dbContext.CampaignCategories, "CategoryId", "CategoryName"));
            

            ViewData["CategoriesCollection"] = categories;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("CategoryId")] ShowCampaignsViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownListToSelectCategory();

                // Something is wrong with the viewmodel.  So, just return it back to the view with the ModelState errors!
                return View(viewmodel);
            }

            // Now performing server-side validation - checking if any books exist for the selected category
            bool CampaignsExist = _dbContext.Campaigns.Any(b => b.CategoryId == viewmodel.CategoryId);
            if (!CampaignsExist)
            {
                //--- Error will be shown as part of the Validation Summary
                ModelState.AddModelError("", "No campaigns were found for the selected category!");

                //--- Error will be attached to the UI Control mapped by the asp-for attribute.
                // ModelState.AddModelError("CategoryId", "No books were found for this category");

                PopulateDropDownListToSelectCategory();
                return View(viewmodel);         // return the viewmodel with the ModelState errors!
            }
            DonationI donationI = new DonationI();

            int total = 0;
            for (int i = 1; i <= _dbContext.DonationIs.Count(); i++)
            {
                if (donationI.CampaignId == viewmodel.CategoryId)
                {
                    total = donationI.DonationAmount + total;
                }
            }
            ViewBag.Total = total;
            return RedirectToAction(
                actionName: "GetCampaignsOfCategory",
                controllerName: "Campaigns",
                routeValues: new { area = "Events", filterCategoryId = viewmodel.CategoryId });
        }
    }
}
