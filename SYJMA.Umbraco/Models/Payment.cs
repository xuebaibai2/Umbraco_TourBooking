using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class Payment : ConfigurationSection
    {
        [ConfigurationProperty("eps_merchant", IsRequired = true)]
        public string EPS_MERCHANT { get { return this["eps_merchant"] as string; } set { base["eps_merchant"] = value; } }
        [ConfigurationProperty("eps_password", IsRequired = true)]
        public string ESP_PASSWORD { get { return this["eps_password"] as string; } set { base["eps_password"] = value; } }
        [ConfigurationProperty("EPS_TXNTYPE", IsRequired = false)]
        public string EPS_TXNTYPE { get; set; }
        [ConfigurationProperty("EPS_REFERENCEID", IsRequired = false)]
        public string EPS_REFERENCEID { get; set; }
        [ConfigurationProperty("EPS_AMOUNT", IsRequired = false)]
        public string EPS_AMOUNT { get; set; }
        [ConfigurationProperty("EPS_TIMESTAMP", IsRequired = false)]
        public string EPS_TIMESTAMP { get; set; }
        [ConfigurationProperty("EPS_FINGERPRINT", IsRequired = false)]
        public string EPS_FINGERPRINT { get; set; }
        [ConfigurationProperty("eps_resulturl", IsRequired = true)]
        public string EPS_RESULTURL { get { return this["eps_resulturl"] as string; } set { base["eps_resulturl"] = value; } }

        public string Name { get; set; }
    }
}