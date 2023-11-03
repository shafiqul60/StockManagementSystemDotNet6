using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class ProductInfoVm
    {
        public string? Code { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
