using FluentValidation;

namespace Task.Api.Contracts.Categories;

public class CategoryRequestValidator: AbstractValidator<CategoryRequest>
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
