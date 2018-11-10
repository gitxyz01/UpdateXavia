using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VerifyController : Controller
    {
        private ICategoryManagementService _categoryService;
        protected IProductService _productService;
        private ICMSCategoryService _cmsCategoryService;
        private ICMSNewsService _cmsNewsService;
        // GET: Admin/Verify
        public VerifyController() {
            _productService = new ProductService();
            _categoryService = new CategoryManagementService();
            _cmsCategoryService = new CMSCategoryService();
            _cmsNewsService = new CMSNewsService();
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

            return RedirectToAction("Categories");
        }
        public ActionResult CMSCategories()
        {
            var model = _cmsCategoryService.GetCMSCategoriesWaiting();
            foreach (var item in model)
            {     
                if (item.Title.Length > 50)
                {
                    item.Title = item.Title.Substring(0, 50) + "...";
                }
                if (item.Description.Length > 30)
                {
                    item.Description = item.Description.Substring(0, 50) + "...";
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult VerifyCMSCategory(int id, int status)
        {
            bool isSuccess = _cmsCategoryService.VerifyCMSCategory(id, status);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete product fail!");
            }

            return RedirectToAction("CMSCategories");
        }

        public ActionResult CMSNews()
        {
            var model = _cmsNewsService.GetWaitingCMSNews();
            return View(model);
        }
        [HttpPost]
        public ActionResult VerifyCMSNews(int id, int status)
        {
            bool isSuccess = _cmsNewsService.VerifyCMSNews(id, status);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete product fail!");
            }

            return RedirectToAction("CMSNews");
        }
        public ActionResult NewDetail(int id)
        {
            var model = _cmsNewsService.GetCMSNewsById(id);
            return View(model);
        }
    }
}