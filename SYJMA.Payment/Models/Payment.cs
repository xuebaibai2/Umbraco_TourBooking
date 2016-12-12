using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYJMA.Payment.Models
{
    public class Payment
    {
        public Payment()
        {
            TxnList = new TxnList();
        }
        public TxnList TxnList { get; set; }
    }
}
