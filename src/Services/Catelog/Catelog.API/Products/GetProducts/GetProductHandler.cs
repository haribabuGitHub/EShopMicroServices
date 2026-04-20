using Marten.Internal;
using Marten.Linq.QueryHandlers;
using System.Data.Common;
using Weasel.Postgresql;

namespace Catelog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    
    internal class GetProductsQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
