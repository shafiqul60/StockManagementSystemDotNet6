using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.Security;
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
        private readonly IDataProtector _protector;
        public ProductRepo(
                            ApplicationDbContext db, 
                            IDataProtectionProvider dataProtectionProvider,
                            DataProtectionPurposeString dataProtectionPurposeString
                            ) : base(db)
                            {
                                _db = db;
                                _protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.MasterKey);
                            }


        public async Task<IEnumerable<ProductListVm>> GetAllProductsBySp()
        {
            var productList = await _db.ProductListVm.FromSqlRaw("exec SP_GetAllProducts").ToListAsync();
            var list = productList.Select(e =>
            {
                e.EncryptedtId = _protector.Protect(e.Id.ToString());
                return e;
            });

            return list;
        }

    }
}
