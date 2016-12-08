using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SYJMA.Umbraco.Models
{
    public class Invoice
    {
        public string SerialNumber { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string SureName { get; set; }
        
        [Required]
        public string Email { get; set; }

        public string JobTitle { get; set; }

        public string Company { get; set; }

        public string Institution { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
