using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eProject.App.Validations;
using Microsoft.AspNetCore.Http;
using static eProject.Models.User;

namespace eProject.Models.ViewModels
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }

        [Required, StringLength(32)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(32)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Display(Name = "Birth Day")]
        public DateTime DateOfBirthDay { get; set; }

        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string State { get; set; }

        public IList<string> Role { get; set; }

        [Display(Name = "Profile Picture")]
        [ValidationImage]
        public IFormFile? ProfileImage { get; set; }

        public User User { get; set; }
    }
}
