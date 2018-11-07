using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class VerifyController : Controller
    {
        private ICategoryManagementService _categoryService;
        protected IProductService _productService;
        // GET: Admin/Verify
        public VerifyController() {
            _productService = new ProductService();
            _categoryService = new CategoryManagementService();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            var model = _productService.GetProductsVerify();
            return View(model);
        }

        [HttpPost]
        public ActionResult VerifyProduct(int id, int status)
        {
            bool isSuccess = _productService.VerifyProduct(id, status);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete product fail!");
            }

            return RedirectToAction("Products");
        }

        public ActionResult Categories()
        {
            var model = _categoryService.GetAllWaitingCategories();
            return View(model);
        }

        [HttpPost]
        public ActionResult VerifyCategory(int id, int status)
        {
            bool isSuccess = _categoryService.VerifyCategory(id, status);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete product fail!");
            }

            return RedirectToAction("Products");
        }
    }
}