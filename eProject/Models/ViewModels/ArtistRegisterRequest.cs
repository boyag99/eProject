using System;
using System.ComponentModel.DataAnnotations;
using eProject.App.Validations;
using Microsoft.AspNetCore.Http;

namespace eProject.Models.ViewModels
{
    public class ArtistRegisterRequest
    {
        [Required]
        [StringLength(255)]
        public string Biography { get; set; }

        [Required]
        [StringLength(255)]
        public string Exhibition { get; set; }

        [Required]
        [Display(Name = "Profile Picture")]
        [ValidationImage]
        public IFormFile ProfileImage { get; set; }

        public bool Accept { get; set; }
    }
}
