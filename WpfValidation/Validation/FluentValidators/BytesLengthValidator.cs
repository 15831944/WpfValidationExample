using System.Text;
using FluentValidation.Validators;

namespace RanOpt.iBuilding.Common.UI.Validation.FluentValidators
{
    internal class BytesLengthValidator : LengthValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return true;

            var min = Min;
            var max = Max;

            if (MaxFunc != null && MinFunc != null)
            {
                max = MaxFunc(context.Instance);
                min = MinFunc(context.Instance);
            }

            var length = Encoding.Default.GetByteCount(context.PropertyValue.ToString());

            if (length < min || (length > max && max != -1))
            {
                context.MessageFormatter
                       .AppendArgument("MinLength", min)
                       .AppendArgument("MaxLength", max)
                       .AppendArgument("TotalLength", length);

                return false;
            }

            return true;
        }

        public BytesLengthValidator(int min, int max) : base(min, max)
        {
        }
    }
}