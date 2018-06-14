using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TokenContent { get; set; }
        public DateTime TimeExpiry { get; set; }
    }
}