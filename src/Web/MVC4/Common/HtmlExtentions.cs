using CP.NLayer.Resources.UI;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CP.NLayer.Web.Mvc4.Common
{
    public static class HtmlExtentions
    {
        public static HtmlString Truncate(this HtmlHelper htmlHelper, string message, int length = 50)
        {
            message = message ?? string.Empty;
            if (message.Length > length)
            {
                message = message.Substring(0, length) + "...";
            }

            return new HtmlString(message);
        }

        public static string PrefixControllerName(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.ControllerName() + name;
        }

        public static string ActionName(this HtmlHelper htmlHelper)
        {
            var name = htmlHelper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            return name ?? string.Empty;
        }

        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            var name = htmlHelper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            return name ?? string.Empty;
        }

        public static MvcHtmlString DisplayOrTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var action = htmlHelper.ActionName();
            if (action.EqualsOp(Operation.Edit) || action.EqualsOp(Operation.Create))
            {
                return MvcHtmlString.Create(htmlHelper.TextBoxFor(expression, htmlAttributes).ToString() +
                                            htmlHelper.ValidationMessageFor(expression).ToString());
            }
            else
            {
                return htmlHelper.DisplayFor(expression);
            }
        }

        public static MvcHtmlString DisplayOrTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var action = htmlHelper.ActionName();
            if (action.EqualsOp(Operation.Edit) || action.EqualsOp(Operation.Create))
            {
                return MvcHtmlString.Create(htmlHelper.TextAreaFor(expression, htmlAttributes).ToString() +
                                            htmlHelper.ValidationMessageFor(expression).ToString());
            }
            else
            {
                return htmlHelper.DisplayFor(expression);
            }
        }

        public static MvcHtmlString DisplayOrEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string templateName = null, string htmlFieldName = null, object additionalViewData = null)
        {
            var action = htmlHelper.ActionName();
            if (action.EqualsOp(Operation.Edit) || action.EqualsOp(Operation.Create))
            {
                return MvcHtmlString.Create(htmlHelper.EditorFor(expression, templateName, htmlFieldName, additionalViewData).ToString() +
                                            htmlHelper.ValidationMessageFor(expression).ToString());
            }
            else
            {
                return htmlHelper.DisplayFor(expression, templateName, htmlFieldName, additionalViewData);
            }
        }

        public static MvcHtmlString RenderSubmit(this HtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewContext.HttpContext.Request.IsAjaxRequest())
            {
                return MvcHtmlString.Empty;
            }

            var tags = new StringBuilder();
            var action = htmlHelper.ActionName();

            TagBuilder submitTag = new TagBuilder("input");
            submitTag.MergeAttribute("type", "submit");

            if (action.EqualsOp(Operation.Create))
            {
                submitTag.MergeAttribute("value", UiResources.Create);
            }

            if (action.EqualsOp(Operation.Delete))
            {
                submitTag.MergeAttribute("value", UiResources.Delete);
            }

            if (action.EqualsOp(Operation.Edit))
            {
                submitTag.MergeAttribute("value", UiResources.Save);
            }

            if (!action.EqualsOp(Operation.Details))
            {
                tags.Append(submitTag.ToString(TagRenderMode.SelfClosing));
                tags.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            }

            tags.Append(htmlHelper.ActionLink(UiResources.GoBack, "Index"));

            return MvcHtmlString.Create(tags.ToString());
        }

        //public static MvcHtmlString OpenFormDialog(this HtmlHelper html, Operation opType, string innerText, object htmlAttributes = null)
        //{
        //    var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
        //    var url = urlHelper.Action(opType.ToString()).ToLower();
        //    var targetTableID = html.ControllerGridId();
        //    TagBuilder tagBuilder = new TagBuilder("button");
        //    tagBuilder.MergeAttribute("data-popMode", "formDialog");
        //    tagBuilder.MergeAttribute("data-opType", opType.ToString().ToLower());
        //    tagBuilder.MergeAttribute("data-url", url);
        //    tagBuilder.MergeAttribute("data-targetTableID", targetTableID);
        //    tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
        //    tagBuilder.InnerHtml = (string.IsNullOrEmpty(innerText)) ? opType.ToString() : HttpUtility.HtmlEncode(innerText);
        //    return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        //}

        //public static MvcHtmlString CrudButtons(this HtmlHelper html)
        //{
        //    return MvcHtmlString.Create
        //    (
        //        html.OpenFormDialog(Operation.Create, WResources.Create).ToHtmlString() +
        //        html.OpenFormDialog(Operation.Edit, WResources.Edit).ToHtmlString() +
        //        html.OpenFormDialog(Operation.Details, WResources.Details).ToHtmlString() +
        //        html.OpenFormDialog(Operation.Delete, WResources.Delete).ToHtmlString()
        //    );
        //}

        //public static MvcHtmlString PopupFormLink(this HtmlHelper html, string innerHtml, Operation opType, object routeValues, object htmlAttributes = null)
        //{
        //    var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
        //    var url = urlHelper.Action(opType.ToString(), new RouteValueDictionary(routeValues));
        //    var targetTableID = html.PrefixControllerName("Table");
        //    return PopupFormLink(html, innerHtml, url, opType, targetTableID, htmlAttributes);
        //}

        //public static MvcHtmlString PopupFormLink(this HtmlHelper html, string innerHtml, string url, Operation opType, string targetTableID, object htmlAttributes = null)
        //{
        //    TagBuilder a = new TagBuilder("a");
        //    a.MergeAttribute("href", url);
        //    a.MergeAttribute("data-popMode", "formDialog");
        //    a.MergeAttribute("data-opType", opType.ToString().ToLower());
        //    a.MergeAttribute("data-targetTableID", targetTableID);
        //    a.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
        //    a.InnerHtml = (string.IsNullOrEmpty(innerHtml)) ? opType.ToString() : innerHtml;
        //    return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        //}

        //public static MvcHtmlString TableContainerDiv(this HtmlHelper html, string dataSourceUrl = null, object htmlAttributes = null)
        //{
        //    var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
        //    var url = dataSourceUrl ?? urlHelper.Action("list");

        //    TagBuilder div = new TagBuilder("div");
        //    div.MergeAttribute("id", html.PrefixControllerName("TableContainer"));
        //    div.MergeAttribute("data-source", url);
        //    div.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
        //    return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        //}

        //public static MvcHtmlString SelectAllCheckBox(this HtmlHelper html, object labelHtmlAttributes = null, object chkHtmlAttributes = null)
        //{
        //    TagBuilder checkbox = new TagBuilder("input");
        //    checkbox.MergeAttribute("type", "checkbox");
        //    checkbox.MergeAttribute("id", html.PrefixControllerName("SelectAll"));
        //    checkbox.MergeAttribute("data-selectAllOnTable", html.PrefixControllerName("Table"));
        //    checkbox.MergeAttributes(new RouteValueDictionary(chkHtmlAttributes), true);

        //    TagBuilder label = new TagBuilder("label");
        //    label.MergeAttribute("for", html.PrefixControllerName("SelectAll"));
        //    label.MergeAttributes(new RouteValueDictionary(labelHtmlAttributes), true);
        //    label.MergeAttribute("class", "button");
        //    label.InnerHtml = WResources.Ui_SelectAll;

        //    return MvcHtmlString.Create(checkbox.ToString(TagRenderMode.SelfClosing) + label.ToString());
        //}

        //public static MvcHtmlString DeleteSelectedButton(this HtmlHelper html, object htmlAttributes = null)
        //{
        //    var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
        //    var url = urlHelper.Action("DeleteSelected");

        //    TagBuilder button = new TagBuilder("button");
        //    button.MergeAttribute("id", html.PrefixControllerName("DeleteSelected"));
        //    button.MergeAttribute("data-deleteSelectedOnTable", html.PrefixControllerName("Table"));
        //    button.MergeAttribute("data-postUrl", url);
        //    button.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
        //    button.InnerHtml = WResources.Ui_DeleteSelected;

        //    return MvcHtmlString.Create(button.ToString());
        //}

        //public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty[]>> expression, MultiSelectList multiSelectList, object htmlAttributes = null)
        //{
        //    //Derive property name for checkbox name
        //    MemberExpression body = expression.Body as MemberExpression;
        //    string propertyName = body.Member.Name;

        //    //Get currently select values from the ViewData model
        //    TProperty[] list = expression.Compile().Invoke(html.ViewData.Model);

        //    //Convert selected value list to a List<string> for easy manipulation
        //    List<string> selectedValues = new List<string>();

        //    if (list != null)
        //    {
        //        selectedValues = new List<TProperty>(list).ConvertAll<string>(delegate(TProperty i) { return i.ToString(); });
        //    }

        //    //Create div
        //    TagBuilder divTag = new TagBuilder("div");
        //    divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

        //    //Add checkboxes
        //    foreach (SelectListItem item in multiSelectList)
        //    {
        //        divTag.InnerHtml += String.Format("<div><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
        //                                            "value=\"{1}\" {2} /><label for=\"{0}_{1}\">{3}</label></div>",
        //                                            propertyName,
        //                                            item.Value,
        //                                            selectedValues.Contains(item.Value) ? "checked=\"checked\"" : "",
        //                                            item.Text);
        //    }

        //    return MvcHtmlString.Create(divTag.ToString());
        //}

        ///// <summary>
        ///// Creating a DropDownList helper for enums
        ///// See http://blogs.msdn.com/b/stuartleeks/archive/2010/05/21/asp-net-mvc-creating-a-dropdownlist-helper-for-enums.aspx
        ///// for Implementing EnumDropDownListFor, Nullable Enum Support
        ///// </summary>
        ///// <typeparam name="TEnum"></typeparam>
        ///// <param name="html"></param>
        ///// <param name="name"></param>
        ///// <param name="selectedValue"></param>
        ///// <returns></returns>
        //public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper html, string name, TEnum selectedValue)
        //{
        //    IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        //    IEnumerable<SelectListItem> items = from value in values
        //                                        select new SelectListItem
        //                                        {
        //                                            Text = value.ToString(),
        //                                            Value = value.ToString(),
        //                                            Selected = value.Equals(selectedValue)
        //                                        };

        //    return html.DropDownList(name, items);
        //}
    }
}