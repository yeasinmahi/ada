using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class ExternalLoginEmail
    {
        public int Id { get; set; }
        public string Google { get; set; }
        public string Facebook { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}