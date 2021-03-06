﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
namespace FitnessClub.Filters
{
    public class BasicAuthenticationIdentity:GenericIdentity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public BasicAuthenticationIdentity(string userName, string password):base(userName,"Basic")
        {
            Password = password;
            UserName = userName;
        }
    }
}