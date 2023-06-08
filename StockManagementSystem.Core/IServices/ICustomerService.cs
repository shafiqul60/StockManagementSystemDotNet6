using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.IServices
{
    public interface ICustomerService 
    {
        Task<bool> CreateCustomer(Customer model);
        Task<List<Customer>> GetAllCustomer();
        Task<List<Customer>> GetAllActiveCustomer();
        Task<Customer> GetCustomer(int? Id);
        Task<bool> UpdateCustomer(Customer model);
        Task<bool> DeleteCustomer(int id);
    }
}
