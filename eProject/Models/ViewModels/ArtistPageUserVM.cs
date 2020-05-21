using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace eProject.Models.ViewModels
{
    public class ArtistPageUserVM
    {
        public SearchArtistRequest SearchArtistRequest { get; set; }

        public IPagedList<User> Users { get; set; }
    }
}
