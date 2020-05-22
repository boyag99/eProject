using System;
using System.ComponentModel.DataAnnotations;

namespace eProject.Models.ViewModels
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
