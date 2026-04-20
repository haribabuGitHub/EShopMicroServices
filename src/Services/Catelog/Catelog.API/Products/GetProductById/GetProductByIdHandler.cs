using Catelog.API.Exceptions;
using Marten.Internal;
using Marten.Linq.QueryHandlers;
using System.Data.Common;
using Weasel.Postgresql;

namespace Catelog.API.Products.GetProductsById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    
    internal class GetProductByIdQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            return product == null ? throw new ProductNotFoundException() : new GetProductByIdResult(product);
        }
    }
}
