﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class ExternalUserDto
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}