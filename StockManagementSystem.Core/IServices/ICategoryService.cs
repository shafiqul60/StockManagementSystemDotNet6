using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagementSystem.Core.IServices
{
    public interface ICategoryService 
    {
        Task<bool> CreateCategory (Category model);
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategory(int? Id);
        Task<bool> UpdateCategory(Category model);
        Task<bool> DeleteCategory(int id);
        Task<List<Category>> GetActiveCategory();

    }
}
