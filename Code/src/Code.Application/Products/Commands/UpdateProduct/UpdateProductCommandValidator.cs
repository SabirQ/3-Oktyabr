namespace Code.Application.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateProductCommandValidator(IApplicationDbContext context)
    {
        this._context = context;

        RuleFor(v => v.ProductName)
           .NotEmpty().WithMessage("ProductName is requried")
           .MaximumLength(200).WithMessage("ProductName must not exceed 200 characters")
           .MustAsync(BeUniqName).WithMessage("The specified ProductName already exists");
        RuleFor(v => v.UnitPrice).NotEmpty().WithMessage("UnitPrice is requried")
            .LessThanOrEqualTo(3000).WithMessage("UnitPrice Should be less or equal than 3000")
            .GreaterThanOrEqualTo(1).WithMessage("UnitPrice Should be more or equal than 1");
        RuleFor(v => v.UnitsInStock).NotNull().WithMessage("UnitsInStock is requried")
             .LessThanOrEqualTo((short)30000).WithMessage("UnitsInStock Should be less or equal than 30000")
            .GreaterThanOrEqualTo((short)0).WithMessage("UnitsInStock Should be more or equal than 0 ");
    }

    public async Task<bool> BeUniqName(UpdateProductCommand model, string productName, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.Id != model.Id)
            .AllAsync(x => x.ProductName != productName, cancellationToken);
    }
}
