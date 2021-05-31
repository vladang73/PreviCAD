using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Previgesst.Validation
{
    public static class ValidationGroupExtensions
    {
        private const string ValidationGroupHiddenFieldKey = "ValidationGroupExtensions::ValidationGroupHiddenField";

        public static MvcHtmlString SubmitForValidationGroup(this HtmlHelper html, string validationGroupName, string value)
        {
            return SubmitForValidationGroup(html, validationGroupName, value, null);
        }

        public static MvcHtmlString SubmitForValidationGroup(this HtmlHelper html, string validationGroupName, string value, object attributes)
        {
            var output = new StringBuilder();
            string hiddenFieldId = null;
            if (!html.ViewData.Keys.Contains(ValidationGroupHiddenFieldKey))
            {
                var hiddenFieldName = $"{validationGroupName}ButtonSource";
                hiddenFieldId = TagBuilder.CreateSanitizedId(hiddenFieldName);

                var hiddenField = new TagBuilder("input");
                hiddenField.MergeAttribute("type", "hidden");
                hiddenField.MergeAttribute("name", hiddenFieldName);
                hiddenField.MergeAttribute("id", hiddenFieldId);
                hiddenField.MergeAttribute("value", String.Empty);
                output.Append(hiddenField.ToString());

                html.ViewData[ValidationGroupHiddenFieldKey] = hiddenFieldId;
            }
            else
            {
                hiddenFieldId = html.ViewData[ValidationGroupHiddenFieldKey] as string;
            }

            var button = new TagBuilder("input");
            button.MergeAttribute("type", "submit");
            button.MergeAttribute("name", validationGroupName);
            button.MergeAttribute("value", value);
            button.MergeAttribute("data-button-source-fieldid", hiddenFieldId);
            if (attributes != null)
            {
                var attributesDictionary = new RouteValueDictionary(attributes);
                button.MergeAttributes(attributesDictionary);
            }            
            output.Append(button.ToString());

            return MvcHtmlString.Create(output.ToString());
        }
    }
}
