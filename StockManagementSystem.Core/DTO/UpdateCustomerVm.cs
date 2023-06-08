using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class UpdateCustomerVm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+88|01)?\d{11}$", ErrorMessage = "Invalid Mobile Number")]
        [Phone]
        public string ContactNumber { get; set; }

        [RegularExpression(@"^(?:\+?88)?01[3-9]\d{8}$", ErrorMessage = "Invalid Bangladeshi telephone number.")]
        public string? TelephoneNumber { get; set; }

        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please Enter Correct Email")]
        public string? Email { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required]
        [DisplayName("Active Status")]
        public bool IsActive { get; set; }
    }
}
