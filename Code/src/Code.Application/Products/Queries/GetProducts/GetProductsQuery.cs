using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Code.Application.Products.Queries.GetProducts;
public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var Products = await _context.Products.AsNoTracking()
             .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
             .OrderBy(x => x.ProductName)
             .ToListAsync(cancellationToken);

        return Products;
    }
}
