using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class Leave
    {
        public int Id { get; set; }

        public LeaveType LeaveType { get; set; }
        public byte LeaveTypeId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime Date { get; set; }
        public string LeaveCause { get; set; }
        public string LeaveAddress { get; set; }
    }
}