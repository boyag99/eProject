using System;
using System.ComponentModel.DataAnnotations;

namespace eProject.Models.ViewModels
{
    public class ContactAdminRequest
    {
        [Required]
        public string Message { get; set; }
    }
}
