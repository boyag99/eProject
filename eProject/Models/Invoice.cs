﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        public  Invoice()
            {
           
            InvoiceDetails = new HashSet<InvoiceDetail>();
            }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int Status { get; set; }
        
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string Role { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

    }
}
