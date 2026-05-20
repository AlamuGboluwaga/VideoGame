using FluentValidation;
using VideoGame.Models.DTOs;

namespace VideoGame.Validator
{
    internal sealed class UserValidation :AbstractValidator<UserDTO>
    {
        public UserValidation() {
            RuleFor(x => x.UserName)
           .NotEmpty()
           .WithMessage("UserName  can not be empty")
           .NotNull().WithMessage("null is not acceptable")
           .EmailAddress()
           .WithMessage("Invalid Format");

            RuleFor(x => x.Password)
           .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(3).WithMessage("Password must be at least 3 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter.");
            ;

        }
     
    }
}
