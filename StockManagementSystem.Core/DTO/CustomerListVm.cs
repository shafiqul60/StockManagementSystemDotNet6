using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class CustomerListVm
    {
        public int Id { get; set; }

        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

      
        public string Address { get; set; }

        [Display(Name = "Number")]
        public string ContactNumber { get; set; }

        [DisplayName("Status")]
        public bool IsActive { get; set; }
        public string Email { get; set; }

        [DisplayName("Payable Amount")]
        public decimal PayableAmount { get; set; }

        [DisplayName("Due Amount")]
        public decimal GetableAmount { get; set; }
    }
}
