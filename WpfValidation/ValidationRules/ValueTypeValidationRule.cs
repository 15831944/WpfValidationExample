using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfValidation.ValidationRules
{
    public class ValueTypeValidationRule : ValidationRule
    {
        public TypeCode TypeCode { get; set; }
        public string ErrorContent { get; set; }

        public override ValidationResult Validate(object obj, CultureInfo cultureInfo)
        {
            var text = obj as string;
            try
            {
                _ = Convert.ChangeType(text, TypeCode);
                return ValidationResult.ValidResult;
            }
            catch (FormatException)
            { }
            catch (InvalidCastException)
            { }
            catch (OverflowException)
            { }

            return new ValidationResult(false, ErrorContent);
        }
    }
}