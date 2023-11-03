using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.IServices
{
    public interface IProductService 
    {
        Task<List<Product>> GetActiveProducts();
        Task<bool> CreateProduct (Product model);
        Task<List<Product>> GetAllProduct();
        Task<Product> GetProduct(int? Id);
        Task<bool> UpdateProduct(Product model);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<ProductListVm>> GetAllProductBySp();

        Task<ProductInfoVm> GetProductInfomationBySp(int productId);
    }
}
