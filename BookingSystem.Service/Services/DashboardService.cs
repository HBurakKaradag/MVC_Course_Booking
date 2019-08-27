using BookingSystem.Data.Context;
using BookingSystem.Domain.WebUI.Account;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class DashboardService : ServiceBase
    {
        public List<MenuVM> GetMenu()
        {
            List<MenuVM> menus = new List<MenuVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                menus = context.Menus.ToList().Select(p => p.MapProperties<MenuVM>()).ToList();
            }

            if (menus == null || !menus.Any())
                return null;

            return this.CategorizeMenuItem(menus);
        }

        private List<MenuVM> CategorizeMenuItem(List<MenuVM> menuDTOList)
        {
            List<MenuVM> categorizedMenuList = new List<MenuVM>();
            if (menuDTOList == null || !menuDTOList.Any())
                return null;

            var parents = menuDTOList.Where(p => p.IsActive && p.ParentId == 0).OrderBy(c => c.Order).ToList();
            foreach (var parentItem in parents)
            {
                var currentParentItem = parentItem.MapProperties<MenuVM>();
                AddSubMenu(ref currentParentItem, menuDTOList);
                categorizedMenuList.Add(currentParentItem);
            }
            return categorizedMenuList;
        }

        private void AddSubMenu(ref MenuVM menu, List<MenuVM> menuList)
        {
            var currentMenu = menu;
            var parent = menuList.Where(p => p.IsActive && p.ParentId == currentMenu.Id).OrderBy(p => p.Order).ToList();
            foreach (var childItem in parent)
            {
                var currentChildItem = childItem.MapProperties<MenuVM>();
                currentMenu.SubMenu.Add(currentChildItem);
                AddSubMenu(ref currentChildItem, menuList);
            }
        }
    }
}