using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace Previgesst.Validation
{
    public static class RenderingContextExtensions
    {
        // Note: This functionality may get removed from this library. It is a bit
        // unrelated, and in many cases could be considered a sign of bad practice!
        // Use at your own risk.
        public static InnerModel<TValue> TemplateContextFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression)
        {
            var getter = expression.Compile();
            var model = new InnerModel<TValue>(
                getter(html.ViewData.Model),
                html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix,
                html);

            html.ViewData.TemplateInfo.HtmlFieldPrefix =
                html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));
            return model;
        }

        public class InnerModel<T> : IDisposable
        {
            public InnerModel(
                T model, 
                string originalPrefix, 
                HtmlHelper html)
            {
                Model = model;
                _originalPrefix = originalPrefix;
                _html = html;
            }

            private string _originalPrefix;
            private HtmlHelper _html;
            public T Model { get; set; }

            public void Dispose()
            {
                _html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix =
                    _originalPrefix;
            }
        }
    }
}
