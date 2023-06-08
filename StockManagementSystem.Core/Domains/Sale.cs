using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class Sale : BaseClass
    {
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
            DiscountPercent = 0;
            DueAmount = 0;
            OriginalPrice = 0;
            PriceAfterDiscount = 0;
            PaymentAmount = 0;
        }
        [Required]
        public string InvoiceNo { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public bool PaymentStatus { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        [Required]
        public decimal DiscountPercent { get; set; }
        [Required]
        public decimal PriceAfterDiscount { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [Required]
        public decimal DueAmount { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }

        public decimal CalculatePriceAfterDiscount(decimal originalPrice, decimal discountPercent)
        {
            decimal discountAmount = originalPrice * (discountPercent / 100);
            decimal priceAfterDiscount = originalPrice - discountAmount;
            return priceAfterDiscount;
        }

    }
}
