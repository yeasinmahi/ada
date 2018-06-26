using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailOptions
    {
        public string ToAddress { get; set; }
        public string ToAddressDisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
