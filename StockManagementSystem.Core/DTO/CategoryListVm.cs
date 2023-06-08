using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class CategoryListVm
    {
        public int Id { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Created By")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Active Status")]
        public bool IsActive { get; set; }

    }
}
