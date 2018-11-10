using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Service.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        protected virtual void PopulateStatusDropDownList(Define.Status status = Define.Status.Active)
        {
            IEnumerable<Define.Status> values = Enum.GetValues(typeof(Define.Status)).Cast<Define.Status>();
            IEnumerable<SelectListItem> items = from value in values
                                                where value != Define.Status.Delete && value != Define.Status.WaitingCreate && value != Define.Status.WaitingDelete
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.Status)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == status,
                                                };

            ViewBag.Status = items;
        }
        // GET: Admin/Menu
        MenuFooterService _service = new MenuFooterService();
        [Authorize(Roles = "Xem,Administrator")]
        public ActionResult Index (int menuType = 0)
        {
            var menus = _service.GetMenusForAdmin(menuType);
            return View(menus);
        }

        [Authorize(Roles = "Thêm,Administrator")]
        public ViewResult Create()
        {
            PopulateStatusDropDownList();
            return View();
        }

        [Authorize(Roles = "Thêm,Administrator")]
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            try { 
            _service.CreateMenu(menu);
            return RedirectToAction ("Index");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Sửa,Administrator")]
        public ViewResult Edit(int id)
        {
            PopulateStatusDropDownList();
            var model = _service.GetMenuById(id);
            return View(model);
        }

        [Authorize(Roles = "Sửa,Administrator")]
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            try
            {
                _service.UpdateMenu(menu);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Xóa,Administrator")]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.DeleteMenu(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}