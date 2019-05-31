using System;
using FluentValidation;
using RanOpt.iBuilding.Common.UI.Validation.FluentValidators;

namespace RanOpt.iBuilding.Common.UI.Validation
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> BytesLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
        {
            return ruleBuilder.SetValidator(new BytesLengthValidator(min, max));
        }

        public static IRuleBuilderOptions<T, string> ExcludeInvalidFileChars<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new InvalidFileNameCharsValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> BoundaryBetween<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty from, bool includeFrom, TProperty to, bool includeTo) where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.SetValidator(new BoundaryBetweenValidator(from, to) {IncludeFrom = includeFrom, IncludeTo = includeTo});
        }
    }
}