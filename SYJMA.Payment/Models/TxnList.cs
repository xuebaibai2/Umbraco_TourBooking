using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SYJMA.Payment.Models;

namespace SYJMA.Payment.Models
{
    public class TxnList
    {
        [XmlArray("TxnList")]
        [XmlArrayItem("Txn", typeof(Txn))]
        public Txn[] Txn { get; set; }

        [XmlAttribute]
        public int Count { get; set; }
    }
}
