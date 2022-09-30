using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ngo.Models
{
    [Table("DonationInfos")]
    public class DonationI
    {
        /// <summary>
        /// Donores Primary Key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Donors Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only character is allowded")]

        [MaxLength(80, ErrorMessage = "{0} can contain a maxium of {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} should contain a minimum of {1} characters.")]
        public string DonarName { get; set; }

        [Required(ErrorMessage = "email is required")]
        [Display(Name = "Email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Mobile is required")]
        [Display(Name ="Mobile Number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string Mobile { get; set; }
        /// <summary>
        /// Donation amount must be greater than 500
        /// </summary>
        [Required]
        [Display(Name ="Donation Amount")]
        [Range(500, int.MaxValue, ErrorMessage = "Please enter a amount grater than or equal to {1}")]
        public int DonationAmount { get; set; }

        #region Navigation Properties to the Master Model - Campaign

        /// <summary>
        /// Foraign key realtionship to Campaign Tabel
        /// </summary>
        [Required]

        [Display(Name = "Select Campaign for which you want to donate")]
        public int CampaignId { get; set; }


        [ForeignKey(nameof(DonationI.CampaignId))]
        public Campaign Campaign { get; set; }


        #endregion
    }
}
