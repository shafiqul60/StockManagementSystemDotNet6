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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> CreateCategory(Category model)
        {
            if (model == null)
            { 
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _categoryRepo.AddAsync(model);
            }
            
        }

        public async Task<List<Category>> GetAllCategory()
        {
            return await _categoryRepo.GetAllAsync();
        }


        public async Task<Category> GetCategory(int? Id)
        {          
            return await _categoryRepo.GetAsync(Id);                   
        }

        public async Task<bool> UpdateCategory(Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _categoryRepo.UpdateAsync(model);
            }
        }

        public async Task<bool> DeleteCategory(int Id)
        {
           
            return await _categoryRepo.DeleteAsync(Id);
           
        }

        public async Task<List<Category>> GetActiveCategory()
        {
            var list = await _categoryRepo.GetAllAsync();
            var filterList = list.Where(x => x.IsActive == true).ToList();
            return filterList;
        }

    }
}
