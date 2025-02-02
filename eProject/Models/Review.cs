﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ReviewId { get; set; }

        public string Message { get; set; }

        public ReviewStatus Status { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created_At { get; set; } = DateTime.Now;

        public enum ReviewStatus
        {
            Approved,
            NotApproved
        }

    }
}
