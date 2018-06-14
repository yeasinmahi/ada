using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public RoleGroup RoleGroup { get; set; }
        public int RoleGroupId { get; set; }
        public List<User> Users { get; set; }
    }
}