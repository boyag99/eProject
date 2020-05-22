using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models
{
    [Table("Invoices")]
    public class Invoice
    {
        public  Invoice()
            {
           
            OrderDetails = new HashSet<OrderDetail>();
            }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        
        [StringLength(255)]
        public string SellerId { get; set; }

        [StringLength(255)]
        public string BuyerId { get; set; }

        [ForeignKey(nameof(ShippingAddress))]
        public int? ShippingAddressId { get; set; }

        public virtual ShippingAddress ShippingAddress { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
