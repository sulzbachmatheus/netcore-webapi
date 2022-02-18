using FluentValidation;
using WebApi.Models;

namespace WebApi.Data.Models.Validations
{
    public class ApplicationValidation : AbstractValidator<Application>
    {
        public ApplicationValidation()
        {
            RuleFor(f => f.ApplicationId).NotEmpty();
            RuleFor(f => f.Url).NotEmpty();
            RuleFor(f => f.PathLocal).NotEmpty();
            RuleFor(f => f.DebuggingMode).NotEmpty();
        }
    }
}
