using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SYJMA.Payment.Models
{
    public class Txn
    {
        public Txn()
        {
            CreditCardInfo = new CreditCardInfo();
        }
        public string txnType { get; set; }
        public string txnSource { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string purchaseOrderNo { get; set; }
        public CreditCardInfo CreditCardInfo { get; set; }

        [XmlAttribute]
        public int ID { get; set; }
    }
}
