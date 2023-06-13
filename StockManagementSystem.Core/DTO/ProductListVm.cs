using StockManagementSystem.Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class ProductListVm
    {
        public int Id { get; set; }

        public string? EncryptedtId { get; set; }

        [DisplayName("Product Category")]
        public string CategoryName { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }
       

        public string Unit { get; set; }
        
        public decimal Price { get; set; }

        [DisplayName("Ideal Quantity")]
        public int IdealQuantity { get; set; }

        [DisplayName("Active Status")]
        public bool IsActive { get; set; }
      
    }
}
