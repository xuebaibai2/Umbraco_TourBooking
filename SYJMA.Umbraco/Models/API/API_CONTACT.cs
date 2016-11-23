using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models.API
{
    public class API_CONTACT
    {
        public string SERIALNUMBER { get; set; }
        public string CONTACTTYPE { get; set; }
        public string TITLE { get; set; }
        public string KEYNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string OTHERINITIAL { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string POSTNOMINAL { get; set; }
        public string ENVELOPESALUTATION { get; set; }
        public string LETTERSALUTATION { get; set; }
        public string CONTACTPOSITION { get; set; }
        public string CAREOF { get; set; }
        public string ADDRESSTYPE { get; set; }
        public string COUNTRY { get; set; }
        public string POSTCODE { get; set; }
        public string ADDRESSLINE1 { get; set; }
        public string ADDRESSLINE2 { get; set; }
        public string SUBURB { get; set; }
        public string STATE { get; set; }
        public string SALESREGION { get; set; }
        public string DAYTELEPHONE { get; set; }
        public string EVENINGTELEPHONE { get; set; }
        public string FAXNUMBER { get; set; }
        public string MOBILENUMBER { get; set; }
        public string EMAILADDRESS { get; set; }
        public string PRIMARYCATEGORY { get; set; }
    }
}