using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class Return : BaseClass
    {
        public Return()
        {
            ReturnDetails = new HashSet<ReturnDetail>();
        }
        [Required]
        public string ReturnFrom { get; set; }
        [Required]
        public string ChallanInvoice { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }

    }
}
