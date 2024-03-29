﻿using StockManagementSystem.Core.Domains;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<bool> CreateProduct(Product model)
        {
            if (model == null)
            { 
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _productRepo.AddAsync(model);
            }
            
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await _productRepo.GetAllAsync();
        }


        public async Task<Product> GetProduct(int? Id)
        {          
            return await _productRepo.GetAsync(Id);                   
        }

        public async Task<bool> UpdateProduct(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _productRepo.UpdateAsync(model);
            }
        }

        public async Task<bool> DeleteProduct(int Id)
        {
           
            return await _productRepo.DeleteAsync(Id);
           
        }

        public async Task<IEnumerable<ProductListVm>> GetAllProductBySp()
        {
            return await _productRepo.GetAllProductsBySp();
        }

        public async Task<List<Product>> GetActiveProducts()
        {
            var list = await _productRepo.GetAllAsync();
            return list.Where(x => x.IsActive == true).ToList();            
        }


        public async Task<ProductInfoVm> GetProductInfomationBySp(int productId)
        {
            return await _productRepo.GetProductInfomationBySp(productId);
        }
    }
}
