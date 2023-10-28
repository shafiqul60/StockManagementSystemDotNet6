using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.Domains;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;

namespace StockManagementSystem.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public ProductController(
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
        }

        public async Task<IActionResult> Index()
        {
            var TypeList = await _productService.GetAllProductBySp();            
            return View(TypeList);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = await _categoryService.GetActiveCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVm product)
        {
            if (ModelState.IsValid)
            {
                var fromVm = _mapper.Map<Product>(product);
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
            ViewBag.CategoryList = await _categoryService.GetActiveCategory();

            return View(product);

        }


        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            ViewBag.CategoryList = await _categoryService.GetActiveCategory();

            if( Id > 0 )
            {
                var item = await _productService.GetProduct(Id);
                if (item == null)
                {
                    return NotFound();
                }
                var itemVm = _mapper.Map<UpdateProductVm>(item);
                return View(itemVm);
            }
            else
            {
                return NotFound();
            }
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
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            if (Id > 0)
            {
                var isDelete = await _productService.DeleteProduct(Id);
                if (isDelete)
                {
                    _notyf.Success("Category Successfully Delete");
                }
                else
                {
                    _notyf.Warning("Somthing is wrong");
                }
            }   
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult UploadFiles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                // No files were selected
                return BadRequest("No files selected.");
            }

            // Process each file
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // Perform your file processing logic here
                    // For example, save the file to disk
                    var filePath = "path/to/save/file/" + file.FileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            // Files uploaded successfully
            return Ok("Files uploaded successfully.");
        }

    }
}
