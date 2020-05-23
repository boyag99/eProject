using System;
using System.ComponentModel.DataAnnotations;

namespace eProject.Models.ViewModels
{
    public class ReviewRequest
    {
        [Required]
        [StringLength(255)]
		public string Message { get; set; }
    }
}
