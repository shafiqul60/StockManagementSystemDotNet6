using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Domains.IRepositories
{
    public interface IProductRepo : IGenericRepo<Product>    
    {
       Task<List<Product>> GetProductsWithCategory();
    }
}
