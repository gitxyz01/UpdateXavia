using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Abtract;
using OnlineStore.Model.Repository;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Implements;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OnlineStore.Infractructure.Utility.Define;
using Microsoft.AspNet.Identity;

namespace OnlineStoreMVC.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        private IOrderService service;
        private IOrderRepository repo ;


        public OrderController()
        {
            service = new OrderService();
            repo = new OrderRepository();
        }

        private void GetDropdowlistOrderStatus(OrderStatus option = OrderStatus.Waiting)
        {
            //IEnumerable<Define.OrderStatus> values = Enum.GetValues(typeof(Define.OrderStatus)).Cast<Define.OrderStatus>();
            //IEnumerable<SelectListItem> items = from value in values
            //                                    select new SelectListItem
            //                                    {
            //                                        Text = EnumHelper.GetDescriptionFromEnum((Define.Status)value),
            //                                        Value = ((int)value).ToString(),

            //                                    };

            //ViewBag.OrderStatus = items;
            IEnumerable<Define.OrderStatus> values = Enum.GetValues(typeof(Define.OrderStatus)).Cast<Define.OrderStatus>();
            IEnumerable<SelectListItem> items = from value in values
                            
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((Define.OrderStatus)value),
                                                    Value = ((int)value).ToString(),
                                                    
                                                };

            ViewBag.OrderStatus = items;
        }

        // GET: Admin/ManageOrder
        [Authorize(Roles = "Xem Đơn Hàng,Administrator")]
        public ActionResult Index(long customerId = 0)
        {
            GetDropdowlistOrderStatus();
            var model = service.GetOrdersByCustomer(customerId);

            return View(model);
        }

        [Authorize(Roles = "Xem Đơn Hàng,Administrator")]
        public ActionResult OrderDetails(long orderId)
        {
            GetDropdowlistOrderStatus();
            var model = service.GetOrderByID(orderId);

            return View(model);
        }

        [Authorize(Roles = "Xóa Đơn Hàng,Administrator")]
        public ViewResult ConfirmDelete()
        {
            return View();
        }
        [Authorize(Roles = "Xóa Đơn Hàng,Administrator")]
        public ActionResult Delete(int Id)
        {
            service.Delete(Id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Cập Nhật Đơn Hàng,Administrator")]
        [HttpPost]
        public ActionResult Update(OrderViewModel model)
        {
            GetDropdowlistOrderStatus();
            model.ModifiedBy = User.Identity.GetUserName();
            service.Update(model);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Xem Danh Sách Khách Hàng,Administrator")]
        public ActionResult AllCustomers()
        {
            var model = service.GetListCustomer();
            return View(model);
        }
    }
}