using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Core.DTO;
using StockManagementSystem.Core.IServices;
using StockManagementSystem.Core.Services;

namespace StockManagementSystem.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly IProductService _productService;
        private static DateTime previousDate = DateTime.Now.Date;
        private static int counter = 1;
        public SalesController(IProductService productService)
        {
            _productService= productService;
        }
        public IActionResult Index()
        {
            return View();
        } 
        
        public async Task<IActionResult> Create()
        {
            SaleVm sale = new SaleVm();
            ViewBag.ProductList = await _productService.GetActiveProducts();
            sale.CustomerId = 0;
            sale.InvoiceNo = GenerateUniqueInvoiceNo();
            sale.CreatedDate = DateTime.Now;
            return View(sale);
        }



        [HttpGet]
        public ActionResult getProductRelatedData(int id)
        {
            var productInfo = _productService.GetProductInfomationBySp(id);
            return Json(new { Data = productInfo });       
        }





        #region GenerateInvoice
        static string GenerateUniqueInvoiceNo()
        {
            string prefix = "FA-";
            string currentDate = DateTime.Now.ToString("ddMMyyyy-");
            string uniqueNumbers = GenerateUniqueNumbers();
            return $"{prefix}{currentDate}{uniqueNumbers}";
        }

        static string GenerateUniqueNumbers()
        {
            DateTime currentDate = DateTime.Now.Date;
            if (currentDate > previousDate)
            {
                counter = 1;
                previousDate = currentDate;
            }
            string serialNumber = counter.ToString();
            counter++;
            return serialNumber;
        }
        #endregion



       
    }
}
