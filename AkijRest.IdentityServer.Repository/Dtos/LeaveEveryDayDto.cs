using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class LeaveEveryDayDto
    {
        public string LeaveTypeName { get; set; }

        public int UserName { get; set; }

        public string Date { get; set; }
        public string LeaveCause { get; set; }
        public string LeaveAddress { get; set; }
    }
}