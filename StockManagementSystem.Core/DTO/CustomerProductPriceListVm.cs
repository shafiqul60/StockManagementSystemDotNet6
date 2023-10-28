using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class CustomerProductPriceListVm
    {
        public int Id { get; set; }
        public CustomerVm Customer { get; set; }
        public ProductCreateVm Product { get; set; }
        public decimal Price { get; set; }
    }
}
