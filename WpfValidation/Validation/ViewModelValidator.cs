using System.Linq;
using FluentValidation;

namespace RanOpt.iBuilding.Common.UI
{
    /// <summary>
    /// This is a base class for validating a set of rules associated with a view model. Each
    /// rule ensures that a property on the view model has a value and that it conforms to
    /// some necessary requirements.
    /// </summary>
    /// <typeparam name="T">The type of the view model.</typeparam>
    public class ViewModelValidator<T> : AbstractValidator<T>
    {
        private readonly string _delimiter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelValidator{T}"/> class.
        /// </summary>
        /// <param name="delimiter">
        /// The characters used between each of the error messages for any broken rules.
        /// Typically this is ', ' or the newline characters.
        /// </param>
        protected ViewModelValidator(string delimiter)
        {
            _delimiter = delimiter;
        }

        /// <summary>
        /// Get the set of messages associated with any broken rules for the specified view model instance and property.
        /// </summary>
        /// <param name="model">The view model instance to check for broken rules.</param>
        /// <param name="propertyName">The name of the view model property to check. If the value is <c>null</c> then
        /// all rules are checked.</param>
        /// <returns>
        /// The set of messages associated with the broken rules. Each message is delimited by the characters
        /// set during construction.
        /// </returns>
        public string GetError(T model, string propertyName)
        {
            var result = Validate(model);
            if (result.IsValid)
            {
                return null;
            }

            var errors = result.Errors
                    .Where(failure => string.IsNullOrEmpty(propertyName) || failure.PropertyName == propertyName)
                    .Select(failure => failure.ErrorMessage);

            return string.Join(_delimiter, errors);
        }

        /// <summary>
        /// Determine whether any of the rules setup for the view model have been broken or not.
        /// </summary>
        /// <param name="model">The view model instance to check.</param>
        /// <returns>
        /// <c>True</c> if any validation rules for the view model have been broken, otherwise <c>false</c>.
        /// </returns>
        public bool HasError(T model)
        {
            return !string.IsNullOrEmpty(GetError(model, null));
        }
    }
}
