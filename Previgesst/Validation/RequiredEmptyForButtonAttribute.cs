using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Previgesst.Validation
{
    public sealed class RequiredEmptyForButtonAttribute : ConditionalAttributeBase, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} must be empty when '{1}' is clicked";

        private RequiredAttribute _innerAttribute = new RequiredAttribute();

        public string ValidationGroup { get; set; }
        public string ButtonName { get; set; }

        public RequiredEmptyForButtonAttribute(string validationGroup, string buttonName)
            : this(validationGroup, buttonName, DefaultErrorMessage)
        {
        }

        public RequiredEmptyForButtonAttribute(string validationGroup, string buttonName, string errorMessage)
            : base(errorMessage)
        {
            this.ValidationGroup = validationGroup;
            this.ButtonName = buttonName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // if the relevant button was clicked
            if (ShouldRunValidation(value, this.ValidationGroup, this.ButtonName, validationContext))
            {
                // match => means we should try validating this field
                // note the subtlety here; we're using a Required validator back-to-front;
                // we check it is valid (i.e. filled in) and that means this should fail
                if (_innerAttribute.IsValid(value))
                    // validation failed - return an error
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "mvcvtkrequiredemptyif",
            };

            var depProp = BuildHiddenFieldId(metadata, context as ViewContext);
            var targetValue = (this.ButtonName ?? "").ToString();

            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", targetValue);

            yield return rule;
        }

        private string BuildHiddenFieldId(ModelMetadata metadata, ViewContext viewContext)
        {
            var depProp = QualifyFieldId(metadata, this.ValidationGroup, viewContext);
            return String.Format("{0}ButtonSource", depProp);
        }

        public override string FormatErrorMessage(string name)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessageString))
            {
                return string.Format(this.ErrorMessageString, name, ButtonName);
            }
            return _innerAttribute.FormatErrorMessage(name);
        }
    }
}
