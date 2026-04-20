using BuildingBlocks.CQRS;
using Catelog.API.Products.GetProductsById;


namespace Catelog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(Product Product);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductByCategory").
               Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                    .ProducesProblem(StatusCodes.Status400BadRequest)
                    .WithSummary("Get a product by category").
                     WithDescription("Get a product by category")
                    .WithDescription("Get a product by category");
        }
    }
}
