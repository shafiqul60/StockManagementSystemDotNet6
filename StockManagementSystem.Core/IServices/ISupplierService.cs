using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.IServices
{
    public interface ISupplierService 
    {
        Task<bool> CreateSupplier (Supplier model);
        Task<List<Supplier>> GetAllSupplier();
        Task<List<Supplier>> GetAllActiveSupplier();
        Task<Supplier> GetSupplier(int? Id);
        Task<bool> UpdateSupplier(Supplier model);
        Task<bool> DeleteSupplier(int id);
    }
}
