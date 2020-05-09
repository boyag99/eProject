using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static eProject.Models.User;

namespace eProject.Models.ViewModels
{
    public class UserInfo
    {
        public string UserId { get; set; }

        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Role { get; set; }

        [Display(Name = "Mobile")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public GenderType Gender { get; set; }

        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirthDay { get; set; }

        public string StreetAddress { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Address
        {
            get
            {
                return StreetAddress + ", " + County + ", " + City + ", " + State;
            }
        }

        public string Picture
        {
            get
            {
                return "/backend/images/users/" + ProfileImage;
            }
        }
    }
}
