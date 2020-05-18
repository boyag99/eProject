using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Blog")]

    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }

        public string Photo { get; set; }

        public DateTime PostedDate { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public string Description { get; set; }


    }
}
