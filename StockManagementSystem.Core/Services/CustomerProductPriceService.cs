using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManagementSystem.Core.IServices;

namespace StockManagementSystem.Core.Services
{
    public class CustomerProductPriceService : ICustomerProductPriceService
    {
        private readonly ICustomerProductPriceRepo _customerProductPriceRepo;

        public CustomerProductPriceService(ICustomerProductPriceRepo customerProductPriceRepo)
        {
            _customerProductPriceRepo = customerProductPriceRepo;
        }

        public async Task<bool> CreateCustomerWiseProductPrice(CustomerProductPrice model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _customerProductPriceRepo.AddAsync(model);
            }

        }

        public async Task<List<CustomerProductPrice>> GetAllCustomerWiseProductPrice()
        {
            return await _customerProductPriceRepo.GetAllCustomerWiseProductPrice();
        }


        public async Task<CustomerProductPrice> GetCustomerWiseProductPrice(int? Id)
        {
            return await _customerProductPriceRepo.GetAsync(Id);
        }

        public async Task<bool> UpdateCustomerWiseProductPrice(CustomerProductPrice model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _customerProductPriceRepo.UpdateAsync(model);
            }
        }

        public async Task<bool> DeleteCustomerWiseProductPrice(int Id)
        {

            return await _customerProductPriceRepo.DeleteAsync(Id);

        }

        
    }
}
