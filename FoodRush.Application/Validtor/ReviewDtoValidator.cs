
public class ReviewDtoValidator : AbstractValidator<ReviewDto>
{
    public ReviewDtoValidator()
    {
        RuleFor(r => r.Comment)
            .MaximumLength(350)
            .WithMessage("Comment must be at most 350 characters.");

        RuleFor(r => r.Rating)
            .InclusiveBetween(1m, 5m)
            .WithMessage("Rating must be between 1 and 5.");
    }
}
