using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class ProductCreateVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Code { get; set; }

        public string Model { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int IdealQuantity { get; set; }
      
        [Required]
        [DisplayName("Active Status")]
        public bool IsActive { get; set; }
        public string? Remarks { get; set; }
    }
}
