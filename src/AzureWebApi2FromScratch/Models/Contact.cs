using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureWebApi2FromScratch.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Activity
    {
        public int ActivityId { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
    }
}