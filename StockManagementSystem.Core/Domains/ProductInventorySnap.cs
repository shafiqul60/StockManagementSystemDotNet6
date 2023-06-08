using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class ProductInventorySnap : BaseClass
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int QunetityOnHend { get; set; }
        [Required]
        public int CurrentQuentity { get; set; }
        [Required]
        public int TransactionQuentity { get; set; }
        [Required]
        public char TransactionType { get; set; }
    }
}
