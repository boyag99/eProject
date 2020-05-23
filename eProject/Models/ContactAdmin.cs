using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("ContactAdmins")]
    public class ContactAdmin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactAdminId { get; set; }

        public string Message { get; set; }

        public StatusMessage Status { get; set; } = StatusMessage.NotApproved;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;

        public DateTime Updated_At { get; set; } = DateTime.Now;

        public virtual User User { get; set; }

        public enum StatusMessage
        {
            Approved,
            NotApproved
        }
    }
}
