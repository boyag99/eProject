using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models.ViewModels
{
    public class DepositUserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public double Balance { get; set; }
    }
}
