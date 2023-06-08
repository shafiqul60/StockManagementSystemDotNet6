using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class CustomerProductPrice : BaseClass
    {
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public decimal Price { get; set; }

    }
}
