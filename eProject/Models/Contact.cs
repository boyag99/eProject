using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eProject.Models
{
    
    public class Contact
    {
        public int Id { get; set; }

        public string Address  { get; set; }

        [DataType(DataType.PhoneNumber)]
        public double Telephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public double Fax { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
