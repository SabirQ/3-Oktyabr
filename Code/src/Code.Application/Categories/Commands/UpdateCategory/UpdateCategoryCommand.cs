namespace Code.Application.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }

}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new Exception(""); // TODO: NotFoundException sınıfı hazırlanacak
        }

        entity.CategoryName = request.CategoryName;
        entity.Description = request.Description;
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
