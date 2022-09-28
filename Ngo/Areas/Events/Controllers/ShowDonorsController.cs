using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Ngo.Areas.Events.ViewModels;
using Ngo.Data;
using System.Collections.Generic;
using System.Linq;

namespace Ngo.Areas.Events.Controllers
{
    [Area("Events")]
    public class ShowDonorsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ShowDonorsController> _logger;

        public ShowDonorsController(
            ApplicationDbContext dbContext,
            ILogger<ShowDonorsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {

            PopulateDropDownListToSelectCategory();

            _logger.LogInformation("--- extracted Campaign");
            return View();
        }
        private void PopulateDropDownListToSelectCategory()
        {
            List<SelectListItem> donors = new List<SelectListItem>();
            donors.Add(new SelectListItem
            {
                Text = "----- select a campaign -----",
                Value = "",
                Selected = true
            });
            donors.AddRange(new SelectList(_dbContext.Campaigns, "CampaignId", "CampaignName"));


            ViewData["CategoriesCollection"] = donors;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("CampignId")] ShowVolunteersViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownListToSelectCategory();

                // Something is wrong with the viewmodel.  So, just return it back to the view with the ModelState errors!
                return View(viewmodel);
            }

            // Now performing server-side validation - checking if any books exist for the selected category
            bool DonorsExist = _dbContext.DonationIs.Any(b => b.CampaignId == viewmodel.CampaignId);
            if (!DonorsExist)
            {
                //--- Error will be shown as part of the Validation Summary
                ModelState.AddModelError("", "No Donors were found for the selected campaign!");

                //--- Error will be attached to the UI Control mapped by the asp-for attribute.
                // ModelState.AddModelError("CategoryId", "No books were found for this category");

                PopulateDropDownListToSelectCategory();
                return View(viewmodel);         // return the viewmodel with the ModelState errors!
            }

            return RedirectToAction(
                actionName: "GetDonorsOfCategory",
                controllerName: "DonationIs",
                routeValues: new { area = "Events", filterCategoryId = viewmodel.CampaignId });
        }
        
    }
}
