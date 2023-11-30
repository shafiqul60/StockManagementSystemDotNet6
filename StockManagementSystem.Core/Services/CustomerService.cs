using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.DTO.SpDTO;
using StockManagementSystem.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<bool> CreateCustomer(Customer model)
        {
            if (model == null)
            { 
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _customerRepo.AddAsync(model);
            }
            
        }

        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _customerRepo.GetAllAsync();
        }


        public async Task<List<Customer>> GetAllActiveCustomer()
        {
            return await _customerRepo.GetAllActiveCustomer();
        }


        public async Task<Customer> GetCustomer(int? Id)
        {          
            return await _customerRepo.GetAsync(Id);                   
        }

        public async Task<bool> UpdateCustomer(Customer model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _customerRepo.UpdateAsync(model);
            }
        }

        public async Task<bool> DeleteCustomer(int Id)
        {
           
            return await _customerRepo.DeleteAsync(Id);
           
        }

        public async Task<CustomerInfoVm> GetCustomerInfomationBySp(string number)
        {
            return await _customerRepo.GetCustomerInfomationBySp(number);
        }
    }
}
