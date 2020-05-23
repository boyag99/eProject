using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Abouts")]
    public class About
    {
        
        public int AboutId { get; set; }

        public string Image { get; set; }

        public string Name  { get; set; }

        public string Description { get; set; }

        public string Slogan { get; set; }

        
    }
}
