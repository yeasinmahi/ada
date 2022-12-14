using System;
using System.Collections.Generic;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string CurrentAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string Education { get; set; }
        public string Note { get; set; }

        public List<Role> Roles { get; set; }
        public int SuperVisorId { get; set; }
        public bool Approved { get; set; }
        public ExternalLoginEmail ExternalLoginEmail { get; set; }

    }
}