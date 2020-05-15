using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            Photos = new HashSet<Photo>();
        }
            
        public int ProductId { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public double SalePrice { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public string  Description { get; set; }

        public bool Status { get; set; }

        public bool Featured { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
 

    }
}
