using FluentValidation.Results;

namespace AP.Web.Common.Validations
{
    public interface IValidateModelState
    {
        ValidationResult ValidateResult { get; set; }
    }
}
