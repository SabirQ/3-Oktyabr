using AutoMapper;
using AutoMapper.QueryableExtensions;
using Code.Application.Categories.Queries.GetCategories;


namespace Code.Application.Category.Queries.GetCategory
{
    public record GetCategoryQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.AsNoTracking()
                 .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(x => x.Id == request.Id);
                
            return category;
        }
    }
}
