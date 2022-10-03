namespace Code.Application.Categories.Commands.DeleteCategory;
public record DeleteCategoryCommand(int Id) : IRequest;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.Where(x => x.Id == request.Id)
              .SingleOrDefaultAsync();

        if (entity == null)
        {
            throw new Exception(""); // TODO: Bu alan NotfoundException olarak düzenlenecek
        }

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}
