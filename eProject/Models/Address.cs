using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddressId { get; set; }

        [StringLength(256)]
        public string StreetAddress { get; set; }

        [StringLength(256)]
        public string PostalCode { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string County { get; set; }

        [StringLength(256)]
        public string State { get; set; }
    }
}
