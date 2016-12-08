using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SYJMA.Umbraco.Models
{
    public class GroupCoordinator
    {
        public string SerialNumber { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string SureName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Mobile { get; set; }
        
        public string DaytimeNumber { get; set; }

        public string Address { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }

        public string Postcode { get; set; }
    }
}