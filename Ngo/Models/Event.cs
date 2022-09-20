using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Ngo.Models
{
    [Table("Events")]
    public class Event
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
        public string discription { get; set; }

        public string ImageUrl { get; set; }    

        #region Navigation Properties to the Transaction Model - Donar

        public ICollection<Donar> Donars { get; set; }

        #endregion
    }
}
