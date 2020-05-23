using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models
{
    public class Item

    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }
        public string BuyerId { get; set; }

        public virtual ShippingAddress ShippingAddress { get; set; }

    }
}
