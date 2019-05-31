using System.IO;
using System.Linq;
using FluentValidation.Validators;

namespace RanOpt.iBuilding.Common.UI.Validation.FluentValidators
{
	internal class InvalidFileNameCharsValidator : PropertyValidator
	{
		public InvalidFileNameCharsValidator() : base(@"{PropertyName} can not incude invalid file name characters")
		{ }

		protected override bool IsValid(PropertyValidatorContext context)
		{
			if (context.PropertyValue == null)
				return true;

			var str = (string)context.PropertyValue;
			var invalidChars = Path.GetInvalidFileNameChars();
			return !invalidChars.Any(c => str.Contains(c.ToString()));
		}
	}
}