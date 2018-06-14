using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class LeaveDto
    {        
        public byte LeaveTypeId { get; set; }
        public string UserName { get; set; }

        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string LeaveCause { get; set; }
        public string LeaveAddress { get; set; }
    }
}