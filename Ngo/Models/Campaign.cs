using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Ngo.Models
{
    [Table("Campaigns")]
    public class Campaign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CamaignId { get; set; }


        [Display(Name = "Campaign Name")]
        [Required]
        [StringLength(100)]
        public string CampaignName { get; set; }


        [Display(Name = "Event Discription")]
        [Required]
        [StringLength(1000)]
        public string Discription { get; set; }

      

        public string ImageUrl { get; set; }
        #region Navigation Properties to the Master Model - CampaignCategory


        [Required]

        [Display(Name = "Select The Campaign Category")]
        public int CategoryId { get; set; }


        [ForeignKey(nameof(Campaign.CategoryId))]
        public CampaignCategory CampaignCategory { get; set; }


        #endregion

        #region Navigation Properties to the Transaction Model - Donar

        public ICollection<DonationI> DonationIs { get; set; }

        #endregion

        #region Navigation Properties to the Transaction Model - Volunteer

        public ICollection<Volunteer> Volunteers { get; set; }

        #endregion
    }
}
