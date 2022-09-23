using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ngo.Models
{
    [Table("DonationInfos")]
    public class DonationI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Donors Name")]

        [MaxLength(80, ErrorMessage = "{0} can contain a maxium of {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} should contain a minimum of {1} characters.")]
        public string DonarName { get; set; }

        [Required(ErrorMessage = "email is required")]
        [Display(Name = "Email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string Mobile { get; set; }

        [Required]
        [Range(500, int.MaxValue, ErrorMessage = "Please enter a amount grater than or equal to {1}")]
        public int DonationAmount { get; set; }

        #region Navigation Properties to the Master Model - Campaign


        [Required]

        [Display(Name = "Select Campaign for which you want to donate")]
        public int CampaignId { get; set; }


        [ForeignKey(nameof(DonationI.CampaignId))]
        public Campaign Campaign { get; set; }


        #endregion
    }
}
