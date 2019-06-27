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
using OnlineStore.Model.ViewModel;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Infractructure.Helper;
using OnlineStore.Model.MessageModel;
using PagedList;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class CategoryController : BaseManagementController
    {
        #region Properties

        private ICategoryManagementService categoryService = new CategoryManagementService();

        #endregion

        #region Constructures

        public CategoryController()
        {
            categoryService = new CategoryManagementService();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Create  Category SelectList using as DataSource of ParentId DropDownList 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="id"></param>
        private void PopulateParentCategoryDropDownList(int? parentId = null, int? id = null)
        {
            IEnumerable<ecom_Categories> listCategories;
            IEnumerable<ecom_Categories> categories = categoryService.GetAllCategories();
            if (parentId != null)
            {
                listCategories = categories.Where(c => c.Id != id).ToList();
            }
            else
            {
                listCategories = categories;
            }

            IEnumerable<SelectListItem> items = from category in listCategories
                                                select new SelectListItem
                                                {
                                                    Text = category.Name,
                                                    Value = category.Id.ToString(),
                                                    Selected = category.Id == parentId
                                                };
            ViewBag.ParentId = items;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Get list available category 
        /// </summary>
        /// <param name="keyword">search key</param>
        /// <param name="page">current page index</param>
        /// <returns></returns>
        [Authorize(Roles = "Xem,Administrator")]
        public ActionResult Index(string keyword, int page = 1)
        {
            int totalItems = 0;
            var categories = categoryService.GetCategories(page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, out totalItems);

            IPagedList<SummaryCategoryViewModel> pageCategories = new StaticPagedList<SummaryCategoryViewModel>(categories, page, OnlineStore.Infractructure.Utility.Define.PAGE_SIZE, totalItems);
            return View(pageCategories);
        }

        /// <summary>
        /// Get details of category
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailCategoryViewModel category = categoryService.GetDetailCategory((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        /// <summary>
        /// Create GUI for create a new category
        /// </summary>
        /// <returns></returns> 
        [Authorize(Roles = "Thêm,Administrator")]
        public ActionResult Create()
        {
            PopulateStatusDropDownList();
            PopulateParentCategoryDropDownList();
            return View();
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>if success return to index page or not return to Create page with error message</returns>
        /// 
        [Authorize(Roles = "Thêm,Administrator")]
        [HttpPost]
        public ActionResult Create(CreateCategoryPostRequest category)
        {
            var user = User.Identity.GetUserName();
            category.CreateBy = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Administrator"))
                {
                    category.Status = (int)Define.Status.Active;
                }
                else
                {
                    category.Status = (int)Define.Status.WaitingCreate;
                }
                bool isSuccess = categoryService.AddCategory(category);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Server error", "Add new category fail.");
                }
            }

            return View(category);
        }

        /// <summary>
        /// Create GUI for edit a category
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Sửa,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = categoryService.getCategoryViewModel((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            PopulateStatusDropDownList((Define.Status)category.Status);
            PopulateParentCategoryDropDownList(category.ParentId, id);
            return View(category);
        }

        /// <summary>
        /// Update information of a category
        /// </summary>
        /// <param name="category">information of category need to updated</param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Sửa,Administrator")]
        [HttpPost]
        public ActionResult Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        /// <summary>
        /// Delete a category from system
        /// </summary>
        /// <param name="id">id of category need to delete</param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Xóa,Administrator")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var isAdmin = User.IsInRole("Administrator");
            string deleteBy = User.Identity.GetUserName();
            bool isSuccess = categoryService.DeleteCategory(id, deleteBy, isAdmin);
            if (!isSuccess)
            {
                ModelState.AddModelError("ServerError", "Delete brand fail!");
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Xem,Administrator")]
        public ActionResult Categories()
        {
            PopulateStatusDropDownList();
            PopulateParentCategoryDropDownList();
            return View();
        }
        [Authorize(Roles = "Xem,Administrator")]
        public ActionResult LoadData()
        {
            var model = categoryService.GetCategoriesAdminTy();
            return Json(new
            {
                status = true,
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDetail(int? id) {
        if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         DetailCategoryViewModel category = categoryService.GetDetailCategory((int)id);
            if (category == null)
              {
                    return HttpNotFound();
           }
            return Json(new
            {
                status = true,
                data = category
            }, JsonRequestBehavior.AllowGet);

        }

        [Authorize(Roles = "Thêm,Sửa,Administrator")]
        [HttpPost]
        public ActionResult SaveCategory(CreateCategoryPostRequest category)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
        
            try
            {
                if (category.Id == 0)
                {
                    if (!ModelState.IsValid)
                    {
                        string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";

                        return Json(new { status = false, message = js.Serialize(fail) });
                    }
                    category.CreateBy = User.Identity.GetUserName();
                    categoryService.AddCategory(category);
                    string addSuccess = "Thêm mới Danh Mục Sản Phẩm thành công!";
                    return Json(new { status = true, message = js.Serialize(addSuccess) });
                }
                if (!ModelState.IsValid)
                {
                    string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";
                    return Json(new { status = false, message = js.Serialize(fail) });
                }
                CategoryViewModel updateModel = new CategoryViewModel()
                {
                    Id= category.Id,
                    Name=category.Name,
                    ParentId=category.ParentId,
                    Description=category.Description,
                    Status=category.Status,
                    SortOrder=category.SortOrder
                };
                categoryService.UpdateCategory(updateModel);
                string success = "Cập nhật Danh Mục Sản Phẩm thành công!";
                return Json(new { status = true, message = js.Serialize(success) });
            }
            catch (Exception ex)
            {
                string fail = ex.Message;
                return Json(new { status = false, message = js.Serialize(fail) });
            }

        }
        [Authorize(Roles = "Xóa,Administrator")]
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                bool isAdmin = User.IsInRole("Administrator");
                string deleteBy = User.Identity.GetUserName();
                categoryService.DeleteCategory(id, deleteBy, isAdmin);
                return Json(new
                {
                    status = true,
                    message = "Xóa Danh Mục Sản Phẩm Thành Công !"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            categoryService.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
