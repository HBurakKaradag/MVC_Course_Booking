using BookingSystem.Domain.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookingSystem.WebUI.HtmlExtensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString GenerateMultiLevelMenu(this HtmlHelper helper, string id, List<MenuVM> menuList)
        {
            var menuHtml = new StringBuilder();
            List<MenuVM> parentItems = menuList.Where(a => a.ParentId == 0).OrderBy(p => p.Order).ToList();
            menuHtml.Append("<ul class='sidebar-menu tree' data-widget='tree'>");
            menuHtml.Append("<li class='header'>MENU NAVIGATION</li>");
            foreach (var parent in parentItems)
            {
                menuHtml.Append("<li class='treeview'>");
                menuHtml.AppendFormat(@"<a href='{0}'>
                                            <i class='fa fa-dashboard'></i>
                                            <span>{1}</span>
                                              <span class='pull-right-container'>
                                                   <i class='fa fa-angle-left pull-right'></i>
                                              </span>
                                          </a>", parent.Url, parent.Title);

                List<MenuVM> childItems = parent.SubMenu.Where(a => a.ParentId == parent.Id).OrderBy(c => c.Order).ToList();
                if (childItems.Count > 0)
                    AddChildItem(helper, parent, menuHtml);
                menuHtml.Append("</li>");
            }
            menuHtml.Append("</ul>");

            return MvcHtmlString.Create(menuHtml.ToString());
        }

        private static void AddChildItem(HtmlHelper helper, MenuVM childItem, StringBuilder menuHtml)
        {
            menuHtml.Append("<ul class='treeview-menu'>");
            List<MenuVM> childItems = childItem.SubMenu;
            foreach (MenuVM child in childItems)
            {
                menuHtml.Append(child.SubMenu.Any() ? "<li class='treeview'>" : "<li>");
                menuHtml.AppendFormat(@"<a href='{0}'> <i class='{1}'></i> {2}"
                                                            , new UrlHelper(helper.ViewContext.RequestContext).Action(actionName: child.Url.Split('/')[1]
                                                                                                                    , controllerName: child.Url.Split('/')[0])
                                                            , "fa fa-circle-o"
                                                            , child.Title);
                if (child.SubMenu.Any())
                {
                    menuHtml.Append(@"<span class='pull-right-container'> <i class='fa fa-angle-left pull-right'></i></span>");
                }
                menuHtml.Append("</a>");

                List<MenuVM> subChilds = child.SubMenu.Where(a => a.ParentId == child.Id).OrderBy(c => c.Order).ToList();
                if (subChilds.Count > 0)
                {
                    AddChildItem(helper, child, menuHtml);
                }
                menuHtml.Append("</li>");
            }
            menuHtml.Append("</ul>");
        }
    }
}