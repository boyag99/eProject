using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models
{[Table("InvoiceDatail")]
    public class InvoiceDetail

    {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
