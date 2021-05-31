using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Previgesst.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotEmptyAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IEnumerable;
            return list != null && list.GetEnumerator().MoveNext();
        }
    }
}