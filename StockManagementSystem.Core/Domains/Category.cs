using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    public class Category : BaseClass
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
