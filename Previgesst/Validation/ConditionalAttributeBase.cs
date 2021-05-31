using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Previgesst.Validation
{
    public abstract class ConditionalAttributeBase : ValidationAttribute
    {
        private const string DefaultErrorMessage = "The {0} field was invalid";

        protected ConditionalAttributeBase()
            : this(DefaultErrorMessage)
        {
        }

        protected ConditionalAttributeBase(string errorMessage)
            : base(errorMessage)
        {
        }

        protected bool ShouldRunValidation(
            object value,
            string dependentProperty,
            object targetValue,
            ValidationContext validationContext)
        {
            var dependentvalue = GetDependentFieldValue(dependentProperty, validationContext);

            // compare the value against the target value
            return ((dependentvalue == null && targetValue == null) ||
                (dependentvalue != null && dependentvalue.Equals(targetValue)));
        }
        
        protected object GetDependentFieldValue(string dependentProperty, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(dependentProperty);

            if (field == null)
                throw new MissingMemberException(containerType.Name, dependentProperty);

            // get the value of the dependent property
            var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);
            return dependentvalue;
        }

        protected string QualifyFieldId(ModelMetadata metadata, string fieldId, ViewContext viewContext)
        {
            // build the ID of the property
            var depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(fieldId);
            // unfortunately this will have the name of the current field appended to the beginning,
            // because the TemplateInfo's context has had this fieldname appended to it. Instead, we
            // want to get the context as though it was one level higher (i.e. outside the current property,
            // which is the containing object (our Person), and hence the same level as the dependent property.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
                // strip it off again
                depProp = depProp.Substring(thisField.Length);
            return depProp;
        }
    }
}
