using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.DTO
{
    public class UpdateCategoryVm
    {
        public int Id { get; set; }
        [Required]
        public String CategoryName { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [DisplayName("Active Status")]
        public bool IsActive { get; set; }
    }
}
