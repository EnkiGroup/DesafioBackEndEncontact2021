using FluentValidation;
using TesteBackendEnContact.Controllers.Models;

namespace TesteBackendEnContact.Controllers.Company.Validation
{
    public class SaveCompanyValidator : AbstractValidator<SaveCompanyRequest>
    {
        public SaveCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required")
                .MinimumLength(5).WithMessage("Minimum length must be 5 characteers")
                .MaximumLength(50).WithMessage("Maximum length must be 50 characteers");
        }
    }
}
