using StockManagementSystem.Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class CustomerProductPriceVm
    {
        [Required]
        public int ProductId { get; set; }      
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
