﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;

namespace StockManagementSystem.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public SupplierController(ISupplierService supplierService, IMapper mapper, INotyfService notyf)
        {
            _supplierService = supplierService;
            _mapper = mapper;
            _notyf = notyf; 
        }
        public async Task<IActionResult> Index()
        {           
            var TypeList = await _supplierService.GetAllSupplier();
            var ListVm = _mapper.Map<List<SupplierListVm>>(TypeList);
            return View(ListVm);
        }


        [HttpGet]    
        public async Task<IActionResult> Create()
        {        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Supplier>(model);
                fromVm.CreatedDate = DateTime.Now;
                fromVm.CreatedBy = "Shafiqul";
                fromVm.UpdatedBy = "";
                var isSaved =  await _supplierService.CreateSupplier(fromVm);
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
         
            var item = await _supplierService.GetSupplier(Id);

            if (item == null)
            {
                return NotFound();
            }
            var itemVm = _mapper.Map<UpdateSupplierVm>(item);
            return View(itemVm);

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateSupplierVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Supplier>(model);
                fromVm.UpdatedBy = "Shafiqul";
                fromVm.UpdatedDate = DateTime.Now;
                var isUpdated = await _supplierService.UpdateSupplier(fromVm);
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
           var isDelete = await _supplierService.DeleteSupplier(Id);
            if (isDelete)
            {
                _notyf.Success("Category Successfully Delete");
            }
            return RedirectToAction("Index");
         
        }

    }
}
