using Ngo.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ngo.Areas.Events.ViewModels
{
    public class ShowVolunteersViewModel
    {
        [Display(Name = "Select Campaign:")]
        [Required(ErrorMessage = "Please select a campaign for displaying the Volunteers")]
        public int CampaignId { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
    }
}
