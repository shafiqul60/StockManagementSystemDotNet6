using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Infrastructure.DbContext;


namespace StockManagementSystem.Infrastructure.Repositories
{
    public class SupplierRepo : GenericRepo<Supplier>, ISupplierRepo
    {
        private readonly ApplicationDbContext _db;
        public SupplierRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Supplier>> GetAllActiveSupplier()
        {
            return await _db.Suppliers.Where(p => p.IsActive == true).ToListAsync();
        }
    }
}
