using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Infrastructure.Repositories
{
    public class CustomerProductPriceRepo : GenericRepo<CustomerProductPrice>, ICustomerProductPriceRepo
    {
        private readonly ApplicationDbContext _db;
        public CustomerProductPriceRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       public async Task<List<CustomerProductPrice>> GetAllCustomerWiseProductPrice()
        {
            return await _db.CustomerProductPrices.Include( p=> p.Customer).Include( c=> c.Product).ToListAsync();
        }


    }
}
