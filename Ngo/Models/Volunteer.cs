using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Ngo.Models
{
    public class Volunteer
    {
        [Key]                                                       //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       //Identity Auto generated
        public int VolunteerId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Enter your Name")]

        [MaxLength(80, ErrorMessage = "{0} can contain a maxium of {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} should contain a minimum of {1} characters.")]
        [RegularExpression( @"^[a-zA-Z]+$",ErrorMessage="Only character is allowded")]
        public string VolunteerName { get; set; }   

        [Required(ErrorMessage = "email is required")]
        [Display(Name = "Email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]  //Email Validation
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "City")]

        [MaxLength(80, ErrorMessage = "{0} can contain a maxium of {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} should contain a minimum of {1} characters.")]
        public string City { get; set; }

        #region Navigation Properties to the Master Model - Campaign

        /// <summary>
        /// Foraign key relationship to Campaign Table
        /// </summary>
        [Required]

        [Display(Name = "Select The Campaign Category")]
        public int CampaignId { get; set; }


        [ForeignKey(nameof(Volunteer.CampaignId))]          //Foraign Key
        public Campaign Campaign { get; set; }


        #endregion
    }
}
