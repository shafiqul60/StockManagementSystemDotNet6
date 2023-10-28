using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class Product : BaseClass
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int IdealQuantity { get; set; }       
        public bool IsActive { get; set; }
        public string? Remarks { get; set; }
    }
}
