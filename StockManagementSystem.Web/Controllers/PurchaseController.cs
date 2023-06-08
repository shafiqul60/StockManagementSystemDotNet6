using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.IServices;
using StockManagementSystem.Core.Services;

namespace StockManagementSystem.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public PurchaseController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
        {
            _supplierService = supplierService;
            _categoryService = categoryService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.SupplierList = await _supplierService.GetAllActiveSupplier();
            ViewBag.CategoryList = await _categoryService.GetActiveCategory();
            ViewBag.ProductList = await _productService.GetAllProduct();
            return View();
        }


    }
}
