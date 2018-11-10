using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Model.Context;
using OnlineStore.Service.Interfaces;
using OnlineStore.Service.Implements;
using PagedList;
using OnlineStore.Model.ViewModel;
using OnlineStoreMVC.Models.ImageModels;
using System.IO;
using Microsoft.AspNet.Identity;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class CMSNewsController : BaseManagementController
    {
        private ICMSNewsService _cmsNewsService = new CMSNewsService();
        private ICMSCategoryService _cmsCategoryService = new CMSCategoryService();
        private IProductService _productService = new ProductService();
        
        [NonAction]
        protected virtual List<SelectListItem> PrepareAllCategoriesModel(int selectedItemId = 0)
        {
            var availableCategories = new List<SelectListItem>();
            int totalItems = 0;
            var categories = _cmsCategoryService.GetCMSCategories(1, int.MaxValue, out totalItems);
            categories = categories.Where(x => x.ParentId != 0).ToList();
            foreach (var c in categories)
            {
                if (c.Id != selectedItemId)
                {
                    availableCategories.Add(new SelectListItem
                    {
                        Text = CMSCategoryExtensions.GetFormattedBreadCrumb(c, categories),
                        Value = c.Id.ToString()
                    });
                }
            }

            return availableCategories;
        }

        // GET: /Admin/CMSNews/


        // GET: /Admin/CMSNews/Create
        [Authorize(Roles = "Thêm,Administrator")]
        public ActionResult Create()
        {
            ViewBag.AvailableCategories = PrepareAllCategoriesModel();
            return View();
        }

        // POST: /Admin/CMSNews/Create
        [Authorize(Roles = "Thêm,Administrator")]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CMSNewsView model, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadFile != null && uploadFile.ContentLength > 0)
                    {
                        ImageUpload imageUpload = new ImageUpload { IsScale = false, SavePath = ImageUpload.LoadPathCMSNews };
                        ImageResult imageResult = imageUpload.RenameUploadFile(uploadFile);

                        if (imageResult.Success)
                        {
                            // Add new image to database
                            var photo = new share_Images
                            {
                                ImageName = imageResult.ImageName,
                                ImagePath = imageResult.ImagePath
                            };
                            var imageId = _productService.AddImage(photo);
                            if (imageId != null)
                            {
                                // Add banner
                                model.CoverImageId = imageId.Value;
                            }
                        }
                        else
                        {
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }

                    _cmsNewsService.AddCMSNews(model);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel();
            return View(model);
        }

        // GET: /Admin/CMSNews/Edit/5
        [Authorize(Roles = "Sửa,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _cmsNewsService.GetCMSNewsById(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel(id.Value);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)news.Status);
            return View(news);
        }

        // POST: /Admin/CMSNews/Edit/5
        [Authorize(Roles = "Sửa,Administrator")]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CMSNewsView model, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadFile != null && uploadFile.ContentLength > 0)
                    {
                        ImageUpload imageUpload = new ImageUpload { IsScale = false, SavePath = ImageUpload.LoadPathCMSNews };
                        ImageResult imageResult = imageUpload.RenameUploadFile(uploadFile);

                        if (imageResult.Success)
                        {
                            // Add new image to database
                            var photo = new share_Images
                            {
                                ImageName = imageResult.ImageName,
                                ImagePath = imageResult.ImagePath
                            };
                            var imageId = _productService.AddImage(photo);
                            if (imageId != null)
                            {
                                // Add banner
                                model.CoverImageId = imageId.Value;
                            }
                        }
                        else
                        {
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }

                    _cmsNewsService.EditCMSNews(model);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel(model.Id);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)model.Status);
            return View(model);
        }
        [Authorize(Roles = "Xóa,Administrator")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                string deleteBy = User.Identity.GetUserName();
                _cmsNewsService.DeleteCMSNews(id, deleteBy);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }
        [Authorize(Roles = "Xem,Administrator")]
        public ActionResult Index(int categoryId = 0)
        {
            var news = _cmsNewsService.GetCMSNewsTy(categoryId);
            foreach(var item in news)
            {
                if(item.Title.Length > 50)
                {
                    item.Title = item.Title.Substring(0, 50) + "...";
                }
            }
            return View(news);
        }
        public ActionResult _AdminNewsCategoryPartial()
        {
            var listCmsNews = _cmsNewsService.GetCMSNewsTy(0);
            var total = listCmsNews.Count();
            ViewBag.total = total;
            var newsCategories = _cmsCategoryService.GetCMSCategoriesTy();
            newsCategories = newsCategories.Where(x => x.ParentId != 0).ToList();
            return PartialView(newsCategories);
        }
    }
}
