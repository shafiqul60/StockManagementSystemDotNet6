using StockManagementSystem.Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class SaleVm
    {
        public SaleVm()
        {
            SaleDetails = new HashSet<SaleDetailVm>();
            DiscountPercent = 0;
            DueAmount = 0;
            OriginalPrice = 0;
            PriceAfterDiscount = 0;
            PaymentAmount = 0;
            LessAmount = 0;
        }
        [Required]
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please Enter Correct Email")]
        public string? CustomerEmail { get; set; }
        [Required]
        [RegularExpression(@"^(?:\+88|01)?\d{11}$", ErrorMessage = "Invalid Mobile Number")]
        [Phone]
        public string CustomerPhone { get; set; }
        [Required]
        public char PaymentStatus { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }

        [Required]
        public decimal DiscountPercent { get; set; }
        [Required]
        public decimal PriceAfterDiscount { get; set; }

        public decimal LessAmount { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [Required]
        public decimal DueAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<SaleDetailVm> SaleDetails { get; set; }

        public decimal CalculatePriceAfterDiscount(decimal originalPrice, decimal discountPercent)
        {
            decimal discountAmount = originalPrice * (discountPercent / 100);
            decimal priceAfterDiscount = originalPrice - discountAmount;
            return priceAfterDiscount;
        }
    }
}
