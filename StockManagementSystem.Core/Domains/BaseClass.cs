using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains
{
    
    public abstract class BaseClass
    {
        public BaseClass()
        {
            CreatedDate = DateTime.Now; 
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public  DateTime CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }


    }
}
