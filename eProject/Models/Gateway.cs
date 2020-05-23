using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace eProject.Models
{
    [Table("Gateways")]
    public class Gateway
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GatewayId { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public string Branch { get; set; }

        public string Message { get; set; }

        public string Logo { get; set; }

        public bool Status { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}
