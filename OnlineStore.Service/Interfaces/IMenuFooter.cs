using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IMenuFooter
    {
        List<Menu> GetMenusFooterForHomePage(int menuType);
        List<Menu> GetMenusForAdmin(int menuType = 0);
        void DeleteMenu(int id);
        void UpdateMenu(Menu menu);
        void CreateMenu(Menu menu);
        Menu GetMenuById(int id);
    }
}
