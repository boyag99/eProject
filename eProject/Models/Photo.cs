using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Name { get; set; }

        public int ProductId { get; set; }

        public bool Status { get; set; }
        public bool Featured { get; set; }

        public virtual Product Product { get; set; }

        
    }
}
