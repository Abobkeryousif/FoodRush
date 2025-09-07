
namespace FoodRush.Application.Validtor
{
    public class UpdateMealDtoValidator : AbstractValidator<updateMealDto>
    {
        public UpdateMealDtoValidator()
        {
            RuleFor(n => n.Name)
              .NotEmpty().WithMessage("Meal Name Is Required")
              .MaximumLength(100).WithMessage("Meal Name Can't Exeeded 100 Charcter");

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description Is Required")
                .MaximumLength(500).WithMessage("Description Can't Exeeded 500 Charcter");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

        }
    }
   
}
