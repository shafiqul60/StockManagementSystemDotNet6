using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;

namespace StockManagementSystem.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public CustomerController(ICustomerService customerService, IMapper mapper, INotyfService notyf)
        {
            _customerService = customerService;
            _mapper = mapper;
            _notyf = notyf; 
        }
        public async Task<IActionResult> Index()
        {           
            var TypeList = await _customerService.GetAllCustomer();
            var ListVm = _mapper.Map<List<CustomerListVm>>(TypeList);
            return View(ListVm);
        }


        [HttpGet]    
        public async Task<IActionResult> Create()
        {        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Customer>(model);
                fromVm.CreatedDate = DateTime.Now;
                fromVm.CreatedBy = "Shafiqul";
                fromVm.UpdatedBy = "";
                var isSaved =  await _customerService.CreateCustomer(fromVm);
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
         
            var item = await _customerService.GetCustomer(Id);

            if (item == null)
            {
                return NotFound();
            }
            var itemVm = _mapper.Map<UpdateCustomerVm>(item);
            return View(itemVm);

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCustomerVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Customer>(model);
                fromVm.UpdatedBy = "Shafiqul";
                fromVm.UpdatedDate = DateTime.Now;
                var isUpdated = await _customerService.UpdateCustomer(fromVm);
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
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
           var isDelete = await _customerService.DeleteCustomer(Id);
            if (isDelete)
            {
                _notyf.Success("Category Successfully Delete");
            }
            return RedirectToAction("Index");
         
        }

    }
}
