using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Implements
{
    public class MenuFooterService : IMenuFooter
    {
        OnlineStoreMVCEntities context = new OnlineStoreMVCEntities();
        public void CreateMenu(Menu menu)
        {
            try
            {
                context.Menus.Add(menu);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void DeleteMenu(int id)
        {
            try
            {          
           var model = context.Menus.Find(id);
            if(model!= null)
            {
                context.Menus.Remove(model);
                context.SaveChanges();
            }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public Menu GetMenuById(int id)
        {
            var model = context.Menus.Find(id);
            return model;
        }

        public List<Menu> GetMenusFooterForHomePage(int menuType)
        {
            try
            {
                var model = context.Menus.Where(x => x.MenuType == menuType && x.Status == (int)Define.Status.Active).OrderBy(x=>x.SortOrder).ToList();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<Menu> GetMenusForAdmin(int menuType = 0)
        {
            try
            {
                var model = context.Menus.Where(x =>menuType==0 ||x.MenuType == menuType).ToList();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMenu(Menu menu)
        {
            try
            {
                var model = context.Menus.Find(menu.Id);
                if (model != null)
                {
                    model.Text = menu.Text;
                    model.Link = menu.Link;
                    model.Status = menu.Status;
                    model.SortOrder = menu.SortOrder;
                    model.MenuType = menu.MenuType;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
