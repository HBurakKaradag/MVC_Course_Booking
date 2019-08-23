using BookingSystem.Domain.WebUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BookingSystem.WebUI.HtmlExtensions
{
    public static class HtmlHelperExtensions
    {
        #region Menu

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

        #endregion Menu

        #region HtmlHelpers

        public static IHtmlString BCheckBoxFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool?>> expression, object htmlAttributes = null)
        {
            var _htmlAttr = new RouteValueDictionary(htmlAttributes ?? new Dictionary<string, object>());
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            bool? isChecked = null;
            if (metadata.Model != null)
            {
                bool modelChecked;
                if (bool.TryParse(metadata.Model.ToString(), out modelChecked))
                {
                    isChecked = modelChecked;
                }
            }
            if (!_htmlAttr.ContainsKey("data-model"))
                _htmlAttr.Add("data-model", metadata.PropertyName);

            // data-type
            if (!_htmlAttr.ContainsKey("data-type"))
                _htmlAttr.Add("data-type", metadata.IsNullableValueType ? Nullable.GetUnderlyingType(metadata.ModelType.UnderlyingSystemType).Name : metadata.ModelType.Name);

            return helper.CheckBox(ExpressionHelper.GetExpressionText(expression), isChecked ?? false, _htmlAttr);
        }

        public static IHtmlString BTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
                                                                 Expression<Func<TModel, TProperty>> expression,
                                                                 object htmlAttributes = null)
        {
            var _htmlAttr = new RouteValueDictionary(htmlAttributes ?? new Dictionary<string, object>());

            // Expression üzerinden gönderilen prop içeriğini elde edelim..
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            // default attribute'lar kontrol edilip ekleniyor..
            // Projede bütünlük sağlanması adına çeşitli attribute'lar set ediliyor.

            // placeHolder
            if (!_htmlAttr.ContainsKey("placeholder"))
            {
                string textVal = string.Format("{0} Filter", metadata.DisplayName ?? metadata.PropertyName);
                _htmlAttr.Add("placeholder", textVal);
            }

            // data-model
            if (!_htmlAttr.ContainsKey("data-model"))
                _htmlAttr.Add("data-model", metadata.PropertyName);

            // data-type
            if (!_htmlAttr.ContainsKey("data-type"))
                _htmlAttr.Add("data-type", metadata.ModelType.Name);

            return helper.TextBoxFor(expression: expression, htmlAttributes: _htmlAttr);
        }

        #endregion HtmlHelpers
    }
}