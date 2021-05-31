using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Previgesst.Validation
{
    public sealed class RangeDefinedByFieldsAttribute : ConditionalAttributeBase, IClientValidatable
    {
        private const string DefaultErrorMessage = "The {0} field must be between the {1} and {2} fields";

        public Type ValidationDataType { get; set; }
        public string DependentMinimumProperty { get; set; }
        public string DependentMaximumProperty { get; set; }

        public RangeDefinedByFieldsAttribute(Type validationDataType, string dependentMinimumProperty, string dependentMaximumProperty)
            : this(validationDataType, dependentMinimumProperty, dependentMaximumProperty, DefaultErrorMessage)
        {
        }

        public RangeDefinedByFieldsAttribute(Type validationDataType, string dependentMinimumProperty, string dependentMaximumProperty, string errorMessage)
            : base(errorMessage)
        {
            this.ValidationDataType = validationDataType;
            this.DependentMinimumProperty = dependentMinimumProperty;
            this.DependentMaximumProperty = dependentMaximumProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var min = GetDependentFieldValue(DependentMinimumProperty, validationContext);
            var max = GetDependentFieldValue(DependentMaximumProperty, validationContext);

            if (min == null)
                min = 0;

            if (max != null)
            {
                var innerAttribute = new RangeAttribute(ValidationDataType, min.ToString(), max.ToString());
                if (innerAttribute.IsValid(value))
                    return ValidationResult.Success;
            }

            return new ValidationResult(
                FormatErrorMessage(validationContext.DisplayName),
                new[] { validationContext.MemberName });
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "mvcvtkrangedefinedbyfields",
            };

            var depPropMin = QualifyFieldId(metadata, DependentMinimumProperty, context as ViewContext);
            var depPropMax = QualifyFieldId(metadata, DependentMaximumProperty, context as ViewContext);

            rule.ValidationParameters.Add("dependentpropertyminimum", depPropMin);
            rule.ValidationParameters.Add("dependentpropertymaximum", depPropMax);

            yield return rule;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessageString, name, DependentMinimumProperty, DependentMaximumProperty);
        }
    }
}
