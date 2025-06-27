using BlogStore.EntityLayer.Entities;
using FluentValidation;

namespace BlogStore.BusinessLayer.ValidationRules;

public class ArticleValidator : AbstractValidator<Article>
{

    public ArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Article title is required")
            .MinimumLength(3).WithMessage("Article title must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Article title must not exceed 100 characters");
    }
    
}