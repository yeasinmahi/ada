using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class SessionExpiry
    {
        public int Id { get; set; }
        public int ExpireTime { get; set; }
    }
}