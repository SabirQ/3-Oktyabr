namespace Code.Application.Categories.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateCategoryCommandValidator(IApplicationDbContext context)
    {
        this._context = context;

        RuleFor(v => v.CategoryName)
            .NotEmpty().WithMessage("CategoryName is requried")
            .MaximumLength(200).WithMessage("CategoryName must not exceed 200 characters")
            .MustAsync(BeUniqName).WithMessage("The specified CategoryName alreadt exists");
    }
     
    public async Task<bool> BeUniqName(string categoryName, CancellationToken cancellationToken)
    {
        return await _context.Categories.AllAsync(x => x.CategoryName != categoryName, cancellationToken);
    }
}
