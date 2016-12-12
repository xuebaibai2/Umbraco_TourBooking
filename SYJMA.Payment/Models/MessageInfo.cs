using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYJMA.Payment.Models
{
    public class MessageInfo
    {
        public string messageID { get; set; }
        public string messagTimestamp { get; set; }
        public int timeoutValue { get; set; }
        public string apiVersion { get; set; }
    }
}
