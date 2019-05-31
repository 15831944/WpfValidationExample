using System;
using FluentValidation.Internal;
using FluentValidation.Validators;

namespace RanOpt.iBuilding.Common.UI.Validation.FluentValidators
{
    internal class BoundaryBetweenValidator : ExclusiveBetweenValidator
    {
        /// <summary>
        /// Indicate whether include the start value of range
        /// </summary>
        public bool IncludeFrom { get; set; }
        /// <summary>
        /// Indicate whether include the end value of range
        /// </summary>
        public bool IncludeTo { get; set; }

        public BoundaryBetweenValidator(IComparable from, IComparable to) : base(from, to)
        { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var propertyValue = (IComparable)context.PropertyValue;

            // If the value is null then we abort and assume success.
            // This should not be a failure condition - only a NotNull/NotEmpty should cause a null to fail.
            if (propertyValue == null)
                return true;

            var minResult = IncludeFrom && Comparer.GetComparisonResult(propertyValue, From) >= 0 ||
                            !IncludeFrom && Comparer.GetComparisonResult(propertyValue, From) > 0;
            var maxResult = IncludeTo && Comparer.GetComparisonResult(propertyValue, To) <= 0 ||
                            !IncludeTo && Comparer.GetComparisonResult(propertyValue, To) < 0;

            if (minResult && maxResult)
                return true;

            context.MessageFormatter
                   .AppendArgument("From", From)
                   .AppendArgument("To", To)
                   .AppendArgument("Value", context.PropertyValue);

            return false;
        }
    }
}