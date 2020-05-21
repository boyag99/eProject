using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eProject.Models.User;

namespace eProject.Models.ViewModels
{
    public class SearchArtistRequest
    {
        public GenderType Gender { get; set; }

        public string Country { get; set; }

    }
}
