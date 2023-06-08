using StockManagementSystem.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.IServices
{
    public interface ICustomerProductPriceService
    {
        Task<bool> DeleteCustomerWiseProductPrice(int Id);
        Task<bool> UpdateCustomerWiseProductPrice(CustomerProductPrice model);
        Task<CustomerProductPrice> GetCustomerWiseProductPrice(int? Id);

        Task<List<CustomerProductPrice>> GetAllCustomerWiseProductPrice();
        Task<bool> CreateCustomerWiseProductPrice(CustomerProductPrice model);
    }
}
