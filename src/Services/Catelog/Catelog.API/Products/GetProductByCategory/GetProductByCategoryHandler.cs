using Catelog.API.Exceptions;

namespace Catelog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(Product Product);
    
    internal class GetProductByCategoryQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).
                FirstOrDefaultAsync(cancellationToken);
            return product == null ? throw new ProductNotFoundException() : new GetProductByCategoryResult(product);
        }
    }
}
