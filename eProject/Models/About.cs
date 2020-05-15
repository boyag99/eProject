using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    [Table("About")]
    public class About
    {
        public About()
        {
            Photos = new HashSet<Photo>();
        }

        public int AboutId { get; set; }

        public string Name  { get; set; }

        public string Description { get; set; }

        public string Slogan { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
