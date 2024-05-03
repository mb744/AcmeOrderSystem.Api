using AcmeOrderSystem.Api.Contracts;
using FluentValidation;

namespace AcmeOrderSystem.Api.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator() 
        { 
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
