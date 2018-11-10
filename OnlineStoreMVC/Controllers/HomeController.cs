using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties

        IBannerService _bannerService = new BannerService();
        ICMSNewsService _cmsNewsService = new CMSNewsService();
        ICategoryManagementService _categoryService = new CategoryManagementService();
        MenuFooterService _menuService = new MenuFooterService();

        #endregion

        #region Constructures

        public HomeController()
        {
            _bannerService = new BannerService();
            _cmsNewsService = new CMSNewsService();
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            PopulateNewProductList();
            PopulateBestSellProductList();
            PopulateClassicStyleProductList();
            PopulateModernStyleProductList();
            //PopulateHighPriorityOrderProductList();
            PopulateCategoryList();
            PopulateTopCategoryList();

            ViewBag.Banner2 = _bannerService.GetBanners2ForHomePage();
            ViewBag.BannerPopup = _bannerService.GetActivePopupForHomePage();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.ContentNews = _cmsNewsService.GetCMSNewsById(OnlineStore.Infractructure.Utility.Define.ID_PAGE_INTRODUCTION).ContentNews;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _HeaderPartial()
        {
            PopulateCategoryList();
            PopulateTopCategoryList();
            return PartialView();
        }

        public ActionResult BannerPartial()
        {
            return PartialView(_bannerService.GetBanners1ForHomePage());
        }

        public ActionResult BlogPartial()
        {
            return PartialView(_cmsNewsService.GetCMSNewsForHomePage());
        }
        public ActionResult FooterPartial()
        {
            return PartialView(_categoryService);
        }

        public ActionResult MenuFooter1Partial()
        {
            var menuFooter1 = _menuService.GetMenusFooterForHomePage(1);
            return PartialView(menuFooter1);
        }
        public ActionResult MenuFooter2Partial()
        {
            var menuFooter2 = _menuService.GetMenusFooterForHomePage(2);
            return PartialView(menuFooter2);
        }
        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
    }
}