using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Ngo.Models
{
    [Table("NgoEvents")]
    public class NgoEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }


        [Display(Name = "Event Name")]
        [Required]
        [StringLength(100)]
        public string EventName { get; set; }


        [Display(Name = "Event Discription")]
        [Required]
        [StringLength(1000)]
        public string Discription { get; set; }

        [Display(Name ="Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name ="End Date")]
        [Required]
        public DateTime EndDate { get; set; }



    }
}
