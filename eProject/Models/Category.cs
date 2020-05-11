using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Name { get; set; }

        public bool Status { get; set; }

        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
