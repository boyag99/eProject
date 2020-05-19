using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace eProject.Models
{
    public class User : IdentityUser
    {
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        [StringLength(255)]
        public DateTime DateOfBirthDay { get; set; }

        [StringLength(255)]
        public string ProfileImage { get; set; }

        [StringLength(255)]
        public string Biography { get; set; }

        [StringLength(255)]
        public string Exhibition { get; set; }

        [ForeignKey(nameof(Address))]
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }

        public enum GenderType
        {
            Male = 0,
            Female = 1,
            Other = 2,
        }
    }
}
