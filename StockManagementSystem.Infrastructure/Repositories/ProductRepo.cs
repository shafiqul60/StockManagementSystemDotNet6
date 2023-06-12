using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Infrastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly ApplicationDbContext _db;
        public ProductRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _db.Products.Include(p => p.Category).ToListAsync();  
        }

        public async Task<List<ProductListVm>> GetAllProductsBySp()
        {
            var productList = await _db.ProductListVm.FromSqlRaw("exec SP_GetAllProduct").ToListAsync();

            return productList;
        }

    }
}
