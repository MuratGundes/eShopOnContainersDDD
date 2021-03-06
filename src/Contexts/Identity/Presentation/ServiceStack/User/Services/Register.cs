﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ServiceStack;
using ServiceStack;

namespace eShop.Identity.User.Services
{
    [Api("Identity")]
    [Route("/identity/users", "POST")]
    public class UserRegister : DomainCommand
    {
        public string GivenName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
