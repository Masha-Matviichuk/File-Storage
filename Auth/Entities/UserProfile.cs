﻿using Microsoft.AspNetCore.Identity;

namespace Auth.Entities
{
    public class UserProfile : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}