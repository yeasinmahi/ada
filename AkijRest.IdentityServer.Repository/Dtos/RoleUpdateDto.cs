using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class RoleUpdateDto
    {
        public string UserName { get; set; }
        public string StatusData { get; set; }
    }
}