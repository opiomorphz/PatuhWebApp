using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;

public static class LabelExtensions
{
    public static IHtmlString LabelFor<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression,
        string labelText,
        object htmlAttributes
    )
    {
        var metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
        var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
        return LabelHelper(htmlHelper, metadata, htmlFieldName, labelText, htmlAttributes);
    }

    public static IHtmlString Label(this HtmlHelper htmlHelper, string expression, string labelText, object htmlAttributes)
    {
        var metadata = ModelMetadata.FromStringExpression(expression, htmlHelper.ViewData);
        return LabelHelper(htmlHelper, metadata, expression, labelText, htmlAttributes);
    }

    private static IHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText, object htmlAttributes)
    {
        string str = labelText ?? (metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>()));
        if (string.IsNullOrEmpty(str))
        {
            return MvcHtmlString.Empty;
        }
        TagBuilder tagBuilder = new TagBuilder("label");
        tagBuilder.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
        var attributes = new RouteValueDictionary(htmlAttributes);
        tagBuilder.MergeAttributes(attributes);
        tagBuilder.SetInnerText(str);
        return new HtmlString(tagBuilder.ToString(TagRenderMode.Normal));
    }
}