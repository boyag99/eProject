using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("GeneralSettings")]
    public class GeneralSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeneralId { get; set; }

        public double RegistrationArtistCost { get; set; }

        public double ShippingCost { get; set; }
    }
}
