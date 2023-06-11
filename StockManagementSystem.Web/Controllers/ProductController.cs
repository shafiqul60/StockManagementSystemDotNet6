using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;
using StockManagementSystem.Core.Security;
using StockManagementSystem.Core.Services;

namespace StockManagementSystem.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly IDataProtector _protector;

        public ProductController(
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeString dataProtectionPurposeString,
            ICategoryService categoryService, 
            IProductService ProductService, 
            IMapper mapper, 
            INotyfService notyf
            )
        {
            _notyf = notyf;
            _mapper = mapper;
            _productService = ProductService;
            _categoryService = categoryService;
            _protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.MasterKey);
        }

        public async Task<IActionResult> Index()
        {
            //var TypeList = await _productService.GetProductsWithCategory();
            var TypeList = await _productService.GetAllProductBySp();
            var ListVm = _mapper.Map<List<ProductListVm>>(TypeList);
            var list = ListVm.Select(e => {
                e.EncryptedtId = _protector.Protect(e.Id.ToString());
                return e;
            });
            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = await _categoryService.GetActiveCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Product>(model);
                fromVm.CreatedDate = DateTime.Now;
                fromVm.CreatedBy = "Shafiqul";
                fromVm.UpdatedBy = "";
                var isSaved = await _productService.CreateProduct(fromVm);
                if (isSaved)
                {
                    _notyf.Success("Category Created Successfully");
                }
                return RedirectToAction("Index");
            }
            _notyf.Warning("Data Validation Error!");
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            ViewBag.CategoryList = await _categoryService.GetActiveCategory();
            int RouteId = Convert.ToInt32(_protector.Unprotect(Id));

            var item = await _productService.GetProduct(RouteId);

            if (item == null)
            {
                return NotFound();
            }
            var itemVm = _mapper.Map<UpdateProductVm>(item);
            ViewBag.ProductId = item.Id;
            return View(itemVm);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateProductVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Product>(model);
                fromVm.UpdatedBy = "Shafiqul";
                fromVm.UpdatedDate = DateTime.Now;
                var isUpdated = await _productService.UpdateProduct(fromVm);
                if (isUpdated)
                {
                    _notyf.Success("Category Successfully Updated");
                }
                return RedirectToAction("Index");
            }
            _notyf.Warning("Data Validation Error!");
            return View(model);

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            int ProductId = Convert.ToInt32(_protector.Unprotect(Id));

            var isDelete = await _productService.DeleteProduct(ProductId);
            if (isDelete)
            {
                _notyf.Success("Category Successfully Delete");
            }
            return RedirectToAction("Index");

        }

    }
}
