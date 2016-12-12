using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYJMA.Payment.Models
{
    public class CreditCardInfo
    {
        public int cardNumber { get; set; }
        public string expiryDate { get; set; }
        public int cvv { get; set; }
    }
}
