using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;

namespace StockManagementSystem.Web.Controllers
{
    public class CustomerProductPriceController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerProductPriceService _customerProductPriceService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public CustomerProductPriceController(ICustomerProductPriceService customerProductPriceService, IProductService ProductService, ICustomerService customerService, IMapper mapper, INotyfService notyf)
        {
            _notyf = notyf;
            _mapper = mapper;
            _productService = ProductService;
            _customerService = customerService;
            _customerProductPriceService = customerProductPriceService;
        }

        public async Task<IActionResult> Index()
        {
            var TypeList = await _customerProductPriceService.GetAllCustomerWiseProductPrice();
            var ListVm = _mapper.Map<List<CustomerProductPriceListVm>>(TypeList);
            return View(ListVm);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ProductList = await _productService.GetAllProduct();
            ViewBag.CustomerList = await _customerService.GetAllActiveCustomer();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerProductPriceVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<CustomerProductPrice>(model);
                fromVm.CreatedDate = DateTime.Now;
                fromVm.CreatedBy = "Shafiqul";
                fromVm.UpdatedBy = "";
                var isSaved = await _customerProductPriceService.CreateCustomerWiseProductPrice(fromVm);
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
        public async Task<IActionResult> Update(int? Id)
        {

            var item = await _customerProductPriceService.GetCustomerWiseProductPrice(Id);

            if (item == null)
            {
                return NotFound();
            }
            var itemVm = _mapper.Map<CustomerProductPriceUpdateVm>(item);
            ViewBag.ProductList = await _productService.GetAllProduct();
            ViewBag.CustomerList = await _customerService.GetAllActiveCustomer();
            return View(itemVm);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CustomerProductPriceUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<CustomerProductPrice>(model);
                fromVm.UpdatedBy = "Shafiqul";
                fromVm.UpdatedDate = DateTime.Now;
                var isUpdated = await _customerProductPriceService.UpdateCustomerWiseProductPrice(fromVm);
                if (isUpdated)
                {
                    _notyf.Success("Category Successfully Updated");
                }
                return RedirectToAction("Index");
            }
            _notyf.Warning("Data Validation Error!");
            ViewBag.ProductList = await _productService.GetAllProduct();
            ViewBag.CustomerList = await _customerService.GetAllActiveCustomer();
            return View(model);

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var isDelete = await _customerProductPriceService.DeleteCustomerWiseProductPrice(Id);
            if (isDelete)
            {
                _notyf.Success("Category Successfully Delete");
            }
            return RedirectToAction("Index");

        }
    }
}
