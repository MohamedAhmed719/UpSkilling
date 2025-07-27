using FluentValidation;

namespace Task.Api.Contracts.Books;

public class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator()
    {
        RuleFor(x => x.Price)
            .Must(x => x > 0)
            .WithMessage("Price of book can't be negative");


        RuleFor(x => x.Stock)
            .Must(x => x > 0)
            .WithMessage("Stock of book can't be negative");
    }
}
