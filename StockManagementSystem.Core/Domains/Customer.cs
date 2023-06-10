using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class Customer : BaseClass
    {
        public Customer() 
        {
            PayableAmount = 0;
            GetableAmount = 0;
        }   
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+88|01)?\d{11}$", ErrorMessage = "Invalid Mobile Number")]
        [Phone]
        public string ContactNumber { get; set; }

        [RegularExpression(@"^(\+?880|0)1[13456789][0-9]{8}$", ErrorMessage = "Please enter a valid Bangladeshi telephone number.")]
        public string? TelephoneNumber { get; set; }

        [EmailAddress]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please Enter Correct Email")]
        public string? Email { get; set; }

        [Required]
        public decimal PayableAmount { get; set; }
        [Required]
        public decimal GetableAmount { get; set; }

        public bool IsActive { get; set; }
    }
}
