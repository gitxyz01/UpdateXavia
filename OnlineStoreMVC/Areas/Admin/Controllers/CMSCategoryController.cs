using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Model.Context;
using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using PagedList;
using OnlineStore.Model.ViewModel;
using Microsoft.AspNet.Identity;
using OnlineStore.Infractructure.Utility;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class CMSCategoryController : BaseManagementController
    {
        private ICMSCategoryService _cmsCategoryService = new CMSCategoryService();
        OnlineStoreMVCEntities context = new OnlineStoreMVCEntities();

        [NonAction]
        protected virtual List<SelectListItem> PrepareAllCategoriesModel(int selectedItemId = 0)
        {
            var availableCategories = new List<SelectListItem>();
            int totalItems = 0;
            var categories = _cmsCategoryService.GetCMSCategories(1, int.MaxValue, out totalItems);
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


        // POST: /Admin/CMS_Category/Create

        [Authorize(Roles = "Thêm,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CMSCategoryView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.IsInRole("Administrator"))
                    {
                        model.Status = (int)Define.Status.Active;
                    }
                    else
                    {
                        model.Status = (int)Define.Status.WaitingCreate;
                    }
                    model.CreatedBy = User.Identity.GetUserName();
                    _cmsCategoryService.AddCMSCategory(model);

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



        // POST: /Admin/CMS_Category/Edit/5

        [Authorize(Roles = "Sửa,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CMSCategoryView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cmsCategoryService.EditCMSCategory(model);

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


            var model = context.cms_Categories.Where(x => x.ParentId == id && x.Status != (int)Define.Status.Delete).FirstOrDefault();
            if (model != null)
            {
                return Json(new
                {
                    status = false,
                    message = "Bạn Cần Xóa Danh Mục Con Trước!"
                }, JsonRequestBehavior.AllowGet);
            }
            string deleteBy = User.Identity.GetUserName();
            var isAdmin = User.IsInRole("Administrator");
            bool isSuccess = _cmsCategoryService.DeleteCMSCategory(id, deleteBy, isAdmin);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete brand fail!");
            }
            return Json(new
            {
                status = true,
                message = "Xóa Danh Mục Tin Tức Thành Công!"
            }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Xem,Administrator")]
        public ActionResult Index()
        {
            var model = _cmsCategoryService.GetCMSCategoriesTy();
            var data1 = model.OrderBy(x => x.ParentId).ThenBy(x => x.Title).ToList();

            var stack = new Stack<CMSCategoryView>();
            foreach (var section in model.Where(x => x.ParentId == default(int)).Reverse())
            {
                stack.Push(section);
                data1.RemoveAt(0);
            }
            var output = new List<CMSCategoryView>();
            while (stack.Any())
            {
                var currentSection = stack.Pop();
                var children = model.Where(x => x.ParentId == currentSection.Id).Reverse();
                foreach (var section in children)
                {
                    stack.Push(section);
                    data1.Remove(section);
                }
                output.Add(currentSection);
            }
            data1 = output;
            return View(data1);
        }
        [Authorize(Roles = "Thêm,Administrator")]
        public ActionResult Create()
        {
            ViewBag.AvailableCategories = PrepareAllCategoriesModel();
            return View();
        }
        [Authorize(Roles = "Sửa,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _cmsCategoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            ViewBag.AvailableCategories = PrepareAllCategoriesModel(id.Value);
            PopulateStatusDropDownList((OnlineStore.Infractructure.Utility.Define.Status)category.Status);
            return View(category);
        }
        public ActionResult LoadData()
        {
            var model = _cmsCategoryService.GetCMSCategoriesTy();
            var data1 = model.OrderBy(x => x.ParentId).ThenBy(x => x.Title).ToList();

            var stack = new Stack<CMSCategoryView>();
            foreach (var section in model.Where(x => x.ParentId == default(int)).Reverse())
            {
                stack.Push(section);
                data1.RemoveAt(0);
            }
            var output = new List<CMSCategoryView>();
            while (stack.Any())
            {
                var currentSection = stack.Pop();
                var children = model.Where(x => x.ParentId == currentSection.Id).Reverse();
                foreach (var section in children)
                {
                    stack.Push(section);
                    data1.Remove(section);
                }
                output.Add(currentSection);
            }
            data1 = output;
            return Json(new
            {
                status = true,
                data = data1,
            }, JsonRequestBehavior.AllowGet
            );


        }
    }
}
