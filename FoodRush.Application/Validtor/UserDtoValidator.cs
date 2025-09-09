namespace FoodRush.Application.Validtor
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.firstName)
                .NotEmpty().WithMessage("First Name Is Requierd")
                .MaximumLength(25)
                  .Matches(@"^[a-zA-Z]+$").WithMessage("First name can only contain letters");
    

            RuleFor(u => u.lastName)
                .NotEmpty().WithMessage("Last Name Is Requierd")
                .MaximumLength(25)
                .Matches(@"^[a-zA-Z]+$").WithMessage("Last name can only contain letters");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email Is Requierd")
                .EmailAddress().WithMessage("Invalid Email Format");

            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Phone Number Is Requierd")
                .MaximumLength(20);

            RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Password is required")
          .MinimumLength(8).WithMessage("Password must be at least 8 characters")
          .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter")
          .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter")
          .Matches(@"[0-9]+").WithMessage("Password must contain at least one number");

            RuleFor(u => u.confirmPassword)
                .Equal(u => u.Password).WithMessage("Password Do not match");

            RuleFor(x => x.BirthDate)
      .NotEmpty().WithMessage("Birth date is required")
        .InclusiveBetween(new DateTime(1980, 1, 1), new DateTime(2020, 12, 31)).WithMessage("Your Age Must Be less Then 46 And Bigger Then 5");

        }
    }
}
