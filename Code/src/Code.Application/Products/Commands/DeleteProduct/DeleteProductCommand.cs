namespace Code.Application.Products.Commands.DeleteProduct;
public record DeleteProductCommand(int Id) : IRequest;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.Where(x => x.Id == request.Id)
              .SingleOrDefaultAsync();

        if (entity == null)
        {
            throw new Exception(""); // TODO: Bu alan NotfoundException olarak düzenlenecek
        }

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}
