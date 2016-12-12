using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYJMA.Payment.Models
{
    public class SecurePayMessage
    {
        public SecurePayMessage()
        {
            MessageInfo = new MessageInfo();
            MerchantInfo = new Merchantinfo();
            Payment = new Payment();
        }
        public MessageInfo MessageInfo { get; set; }
        public Merchantinfo MerchantInfo { get; set; }
        public string RequestType { get; set; }
        public Payment Payment { get; set; }
    }
}
