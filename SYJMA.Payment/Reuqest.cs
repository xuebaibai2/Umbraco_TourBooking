using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYJMA.Payment.Models;

namespace SYJMA.Payment
{
    public class Reuqest
    {
        SecurePayMessage request = new SecurePayMessage();
        Serializer serialzer = new Serializer();

        public string NewPayment(CreditCardInfo creditCard)
        {
            SecurePayMessage message = new SecurePayMessage();

            message.MessageInfo.messageID = "";

            return serialzer.GetSerializedXml<SecurePayMessage>(message);
        }
    }
}
