using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("AuctionHistories")]
    public class AuctionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AuctionHistoryId { get; set; }

        public double Bid { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;

    }
}
