using AutoMapper;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;

namespace StockManagementSystem.Web.Configuration
{
    public class MapConfig : Profile
    {

        public MapConfig()
        {
            CreateMap<Category, CategoryVm>().ReverseMap();
            CreateMap<Category, UpdateCategoryVm>().ReverseMap();
            CreateMap<Category, CategoryListVm>().ReverseMap();

            CreateMap<Supplier, SupplierVm>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierVm>().ReverseMap();
            CreateMap<Supplier, SupplierListVm>().ReverseMap();

            CreateMap<Customer, CustomerVm>().ReverseMap();
            CreateMap<Customer, UpdateCustomerVm>().ReverseMap();
            CreateMap<Customer, CustomerListVm>().ReverseMap();

            CreateMap<Product, ProductCreateVm>().ReverseMap();
            CreateMap<Product, UpdateProductVm>().ReverseMap();
        }
       
    }
}
