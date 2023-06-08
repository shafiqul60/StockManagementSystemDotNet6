using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class CategoryVm
    {
        [Required]           
        public String CategoryName { get; set; }
        [DisplayName("Active Status")]
        [Required]
        public bool IsActive { get; set; }

    }

}
