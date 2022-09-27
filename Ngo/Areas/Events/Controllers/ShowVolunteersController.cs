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
    [Area("Events")]
    public class ShowVolunteersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ShowVolunteersController> _logger;

        public ShowVolunteersController(
            ApplicationDbContext dbContext,
            ILogger<ShowVolunteersController> logger)
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
            List<SelectListItem> volunteers = new List<SelectListItem>();
            volunteers.Add(new SelectListItem
            {
                Text = "----- select a campaign -----",
                Value = "",
                Selected = true
            });
            volunteers.AddRange(new SelectList(_dbContext.Campaigns, "CampaignId", "CampaignName"));


            ViewData["CategoriesCollection"] = volunteers;
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
            bool VolunteersExist = _dbContext.Volunteers.Any(b => b.CampaignId == viewmodel.CampaignId);
            if (!VolunteersExist)
            {
                //--- Error will be shown as part of the Validation Summary
                ModelState.AddModelError("", "No Volunteers were found for the selected campaign!");

                //--- Error will be attached to the UI Control mapped by the asp-for attribute.
                // ModelState.AddModelError("CategoryId", "No books were found for this category");

                PopulateDropDownListToSelectCategory();
                return View(viewmodel);         // return the viewmodel with the ModelState errors!
            }
           
            return RedirectToAction(
                actionName: "GetVolunteersOfCategory",
                controllerName: "Volunteers",
                routeValues: new { area = "Events", filterCategoryId = viewmodel.CampaignId });
        }
    }
}
