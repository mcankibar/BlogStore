using BlogStore.EntityLayer.Entities;
using FluentValidation;

namespace BlogStore.BusinessLayer.ValidationRules;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x=>x.CategoryName).NotEmpty().WithMessage("CategoryName is required");
        RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("CategoryName must be at least 3 characters long");
        RuleFor(x => x.CategoryName).MaximumLength(30).WithMessage("CategoryName must not exceed 20 characters");
        
    }
}