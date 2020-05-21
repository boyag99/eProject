using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            Photos = new HashSet<Photo>();
            InvoiceDetails = new HashSet<OrderDetail>();
        }
            
        public int ProductId { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public double SalePrice { get; set; }

        public int Quantity { get; set; } = 1;

        public int Hot { get; set; }

        public DateTime FromDate { get; set; } = DateTime.Now;

        public DateTime ToDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public string  Description { get; set; }

        public bool Status { get; set; }

        public bool Featured { get; set; }

        public bool Auction { get; set; } = false;

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<OrderDetail> InvoiceDetails { get; set; }


    }
}
