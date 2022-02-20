using FluentValidation;
using System.Text.RegularExpressions;
using TesteBackendEnContact.Controllers.Models.Contact;

namespace TesteBackendEnContact.Controllers.Validation.Contact
{
    public class EditContactValidator : AbstractValidator<EditContactRequest>
    {
        public EditContactValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Name is required.")
                .MinimumLength(5).WithMessage("Minimum length must be 5 characteers.")
                .MaximumLength(50).WithMessage("Maximum length must be 50 characteers.");
            RuleFor(x => x.Phone).NotNull().NotEmpty().Must(ValidatorPhone).WithMessage("The phone did not respond to the expected format: (11999998888)");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Email : is not a valid email address."); ;
        }
        private bool ValidatorPhone(string phone)
        {
            var regex = new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$");
            return regex.IsMatch(phone) ? true : false;
        }
    }
}
