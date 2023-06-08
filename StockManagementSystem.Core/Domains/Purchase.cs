using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class Purchase : BaseClass
    {
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
            DueAmount = 0;
            PaymentAmount = 0;
            OriginalPrice = 0;
        }
        [Required]
        public string ChallanNo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ChallanDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public decimal DueAmount { get; set; }
        [Required]
        public bool ChallanStatus { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }    
    }
}
