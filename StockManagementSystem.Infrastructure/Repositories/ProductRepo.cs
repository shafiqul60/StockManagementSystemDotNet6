using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.DTO.SpDTO;
using StockManagementSystem.Infrastructure.DbContext;


namespace StockManagementSystem.Infrastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly ApplicationDbContext _db;
        public ProductRepo( ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<IEnumerable<ProductListVm>> GetAllProductsBySp()
        {
            return await _db.ProductListVm.FromSqlRaw("exec SP_GetAllProducts").ToListAsync();
        }

        public async Task<ProductInfoVm> GetProductInfomationBySp(int productId)
        {
            try
            {
                var productInfo = await _db.ProductInfoVm.FromSqlRaw("exec SP_GetProductInfoById {0}", productId).ToListAsync();
                return productInfo.FirstOrDefault() ?? new ProductInfoVm();
            }
            catch (Exception ex)
            {
                throw;
            }
        
        }
    }
}
