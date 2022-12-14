using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Ngo.Models
{
    [Table("CampaignCategory")]
    public class CampaignCategory
    {
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]               //Identity key
        public int CategoryId { get; set; }


        [Display(Name = "Campaign Name")]
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        #region Navigation Properties to the Transaction Model - Campaign

        public ICollection<Campaign> Campaigns { get; set; }                //ICollection of Campaign

        #endregion

    }
}
