using System;
using System.ComponentModel.DataAnnotations;

namespace eProject.Models.ViewModels
{
    public class UpdateUserAddressRequest
    {
        [Required]
        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string State { get; set; }
    }
}
