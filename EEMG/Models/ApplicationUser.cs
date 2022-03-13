﻿using Microsoft.AspNetCore.Identity;


namespace EEMG.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }

        public ApplicationUser() { }
    }
}
