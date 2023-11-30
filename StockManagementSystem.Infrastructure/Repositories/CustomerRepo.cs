using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.DTO.SpDTO;
using StockManagementSystem.Infrastructure.DbContext;


namespace StockManagementSystem.Infrastructure.Repositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Customer>> GetAllActiveCustomer()
        {
            return await _db.Customers.Where(p => p.IsActive == true).ToListAsync();
        }

        public async Task<CustomerInfoVm> GetCustomerInfomationBySp(string number)
        {
            try
            {
                var customerInfo = await _db.CustomerInfoVm.FromSqlRaw("exec SP_GetCustomerInfoById {0}", number).ToListAsync();
                return customerInfo.FirstOrDefault() ?? new CustomerInfoVm();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
