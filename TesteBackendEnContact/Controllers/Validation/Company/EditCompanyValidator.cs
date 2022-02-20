using FluentValidation;
using TesteBackendEnContact.Controllers.Models;

namespace TesteBackendEnContact.Controllers.Validation.Company
{
    public class EditCompanyValidator : AbstractValidator<EditCompanyRequest>
    {
        public EditCompanyValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id is required");
            RuleFor(x => x.Name)
               .NotNull().WithMessage("Name is required")
               .MinimumLength(5).WithMessage("Minimum length must be 5 characteers")
               .MaximumLength(50).WithMessage("Maximum length must be 50 characteers");
        }
    }
}
