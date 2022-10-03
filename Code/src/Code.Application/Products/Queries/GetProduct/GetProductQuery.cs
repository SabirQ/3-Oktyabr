using AutoMapper;
using AutoMapper.QueryableExtensions;
using Code.Application.Products.Queries.GetProducts;


namespace Code.Application.Products.Queries.GetProductQuery
{
    public record GetProductQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking()
                 .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(x => x.Id == request.Id);

            return product;
        }
    }
}
