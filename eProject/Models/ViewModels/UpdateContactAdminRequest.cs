using System;
using static eProject.Models.ContactAdmin;

namespace eProject.Models.ViewModels
{
    public class UpdateContactAdminRequest
    {
        public int ContactAdminId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }

        public StatusMessage Status { get; set; }

        public ContactAdmin ContactAdmin { get; set; }

        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
