using BookingSystem.Core.Extensions;
using BookingSystem.Domain.WebUI.Account;
using System;
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
                menuHtml.Append("<li class='elementliStatus'>");
                menuHtml.AppendFormat(@"<a href='{0}'>
                                            <i class='fa fa-dashboard'></i>
                                            <span>{1}</span>
                                              <span class='pull-right-container'>
                                                   <i class='fa fa-angle-left pull-right'></i>
                                              </span>
                                          </a>", parent.Url, parent.Title);

                List<MenuVM> childItems = parent.SubMenu.Where(a => a.ParentId == parent.Id).OrderBy(c => c.Order).ToList();
                bool isSelected = false;
                if (childItems.Count > 0)
                    AddChildItem(helper, parent, menuHtml, ref isSelected);
                menuHtml.Append("</li>");
                menuHtml.Replace("elementliStatus", isSelected ? "treeview menu-open active" : "treeview");
            }
            menuHtml.Append("</ul>");

            return MvcHtmlString.Create(menuHtml.ToString());
        }

        private static void AddChildItem(HtmlHelper helper, MenuVM childItem, StringBuilder menuHtml, ref bool isSelected)
        {
            menuHtml.Append("<ul class='treeview-menu'>");
            List<MenuVM> childItems = childItem.SubMenu;
            foreach (MenuVM child in childItems)
            {
                bool childSelected = false;
                if (child.Url.IsNotNull())
                {
                    var urlPath = helper.ViewContext.HttpContext.Session["ActiveMenuPath"].ToString();
                    var pathArray = urlPath?.Split('/').Where(p => p.IsNotNull()).ToArray();

                    if (pathArray.Any())
                    {
                        string controllerName = pathArray.ElementAtOrDefault(0) != null ? pathArray[0] : string.Empty;
                        string actionName = pathArray.ElementAtOrDefault(1) != null ? pathArray[1] : string.Empty;

                        if (string.Format("{0}/{1}", controllerName, actionName) == child.Url)
                        {
                            childSelected = true;
                            isSelected = true;
                        }
                    }
                }

                menuHtml.Append(child.SubMenu.Any() ? "<li class='treeview'>" : "<li class='elementliUrlStatus'>");
                menuHtml.AppendFormat(@"<a href='{0}'> <i class='{1}'></i> {2}"
                                                            , new UrlHelper(helper.ViewContext.RequestContext).Action(actionName: child.Url.Split('/')[1]
                                                                                                                    , controllerName: child.Url.Split('/')[0])
                                                            , "fa fa-circle-o"
                                                            , child.Title);
                menuHtml.Replace("elementliUrlStatus", childSelected ? "active" : string.Empty);

                if (child.SubMenu.Any())
                {
                    menuHtml.Append(@"<span class='pull-right-container'> <i class='fa fa-angle-left pull-right'></i></span>");
                }
                menuHtml.Append("</a>");

                List<MenuVM> subChilds = child.SubMenu.Where(a => a.ParentId == child.Id).OrderBy(c => c.Order).ToList();
                if (subChilds.Count > 0)
                {
                    isSelected = false;
                    AddChildItem(helper, child, menuHtml, ref isSelected);
                }
                menuHtml.Append("</li>");
            }
            menuHtml.Append("</ul>");
        }

        #endregion Menu

        #region HtmlHelpers

        public static IHtmlString BHiddenFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var _htmlAttr = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            if (!_htmlAttr.ContainsKey("data-model"))
                _htmlAttr.Add("data-model", metadata.PropertyName);

            if (!_htmlAttr.ContainsKey("data-type"))
                _htmlAttr.Add("data-type", metadata.ModelType.Name);

            return helper.HiddenFor(expression: expression, htmlAttributes: htmlAttributes);
        }

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

        public static IHtmlString BTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool? isReadonly = null)
        {
            var _htmlAttr = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            if (!_htmlAttr.ContainsKey("readonly") && isReadonly.HasValue)
                _htmlAttr.Add("readonly", isReadonly);

            if (!_htmlAttr.ContainsKey("data-model"))
                _htmlAttr.Add("data-model", metadata.PropertyName);

            // data-type
            if (!_htmlAttr.ContainsKey("data-type"))
                _htmlAttr.Add("data-type", metadata.ModelType.Name);

            return TextAreaExtensions.TextAreaFor(helper, expression, new RouteValueDictionary(_htmlAttr));
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
                string textVal = string.Format("{0}", metadata.DisplayName ?? metadata.PropertyName);
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

        public static IHtmlString BuildBrandCrumb(this HtmlHelper helper)
        {
            var routeData = helper.ViewContext.RouteData;
            StringBuilder _sb = new StringBuilder();
            _sb.Append("<ol class='breadcrumb'>");

            // Get default Route
            var defaultRoute = ((Route)routeData.Route).Defaults;
            var defActionName = defaultRoute["action"].ToString();
            var defControllerName = defaultRoute["controller"].ToString();

            _sb.AppendFormat("<li><a href='{0}'><i class='fa fa-home'></i> {1} </a></li>",
                new UrlHelper(helper.ViewContext.RequestContext).Action(actionName: defActionName, controllerName: defControllerName)
                , defControllerName);

            // Farklı bir sayfa açıldıysa ekleyelim
            if (defaultRoute["controller"] != routeData.Values["controller"] && defaultRoute["action"] != routeData.Values["action"])
            {
                _sb.AppendFormat("<li><a href='{0}'>{1}</a></li>"
                                               , new UrlHelper(helper.ViewContext.RequestContext).Action(actionName: routeData.Values["action"].ToString(),
                                                                                                         controllerName: routeData.Values["controller"].ToString())
                                               , routeData.Values["controller"]);

                _sb.AppendFormat("<li class='active'>{0}</li>"
                                               , helper.ViewBag?.Title);
            }

            _sb.Append("</ol>");

            return MvcHtmlString.Create(_sb.ToString());
        }

        #endregion HtmlHelpers
    }
}