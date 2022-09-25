using Ngo.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ngo.Areas.Events.ViewModels
{
    public class ShowCampaignsViewModel
    {
        [Display(Name = "Select Category:")]
        [Required(ErrorMessage = "Please select a category for displaying the books")]
        public int CategoryId { get; set; }

        public int Total { get; set; }

        public ICollection<CampaignCategory> CampaignCategories { get; set; }
    }
}
