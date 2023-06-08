using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepo _supplierRepo;

        public SupplierService(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public async Task<bool> CreateSupplier(Supplier model)
        {
            if (model == null)
            { 
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _supplierRepo.AddAsync(model);
            }
            
        }

        public async Task<List<Supplier>> GetAllSupplier()
        {
            return await _supplierRepo.GetAllAsync();
        }


        public async Task<List<Supplier>> GetAllActiveSupplier()
        {
            return await _supplierRepo.GetAllActiveSupplier();
        }


        public async Task<Supplier> GetSupplier(int? Id)
        {          
            return await _supplierRepo.GetAsync(Id);                   
        }

        public async Task<bool> UpdateSupplier(Supplier model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _supplierRepo.UpdateAsync(model);
            }
        }

        public async Task<bool> DeleteSupplier(int Id)
        {
           
            return await _supplierRepo.DeleteAsync(Id);
           
        }
    }
}
