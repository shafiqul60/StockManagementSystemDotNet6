using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;

namespace StockManagementSystem.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public CategoryController(ICategoryService categoryService, IMapper mapper, INotyfService notyf)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _notyf = notyf; 
        }
        public async Task<IActionResult> Index()
        {           
            var TypeList = await _categoryService.GetAllCategory();
            var ListVm = _mapper.Map<List<CategoryListVm>>(TypeList);
            return View(ListVm);
        }


        [HttpGet]    
        public async Task<IActionResult> Create()
        {        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Category>(model);
                fromVm.CreatedDate = DateTime.Now;
                fromVm.CreatedBy = "Shafiqul";
                fromVm.UpdatedBy = "";
                var isSaved =  await _categoryService.CreateCategory(fromVm);
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
         
            var item = await _categoryService.GetCategory(Id);

            if (item == null)
            {
                return NotFound();
            }
            var itemVm = _mapper.Map<UpdateCategoryVm>(item);
            return View(itemVm);

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryVm model)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Category>(model);
                fromVm.UpdatedBy = "Shafiqul";
                fromVm.UpdatedDate = DateTime.Now;
                var isUpdated = await _categoryService.UpdateCategory(fromVm);
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
           var isDelete = await _categoryService.DeleteCategory(Id);
            if (isDelete)
            {
                _notyf.Success("Category Successfully Delete");
            }
            return RedirectToAction("Index");
         
        }

    }
}
