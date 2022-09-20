using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace Ngo.Models
{
    public class Donar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonarId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Donars Name")]

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

        #region Navigation Properties to the Master Model - Event


        [Required]

        [Display(Name = "Select Event for which you want to donate")]
        public int EventId { get; set; }


        [ForeignKey(nameof(Donar.EventId))]
        public Event Event { get; set; }


        #endregion



    }
}
