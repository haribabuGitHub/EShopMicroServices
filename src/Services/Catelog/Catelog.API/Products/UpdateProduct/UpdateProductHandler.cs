using Catelog.API.Exceptions;
using FluentValidation;

namespace Catelog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Production id is required");
            RuleFor(command => command.name).NotEmpty().WithMessage("Name is Required").Length(2, 150).WithMessage("Name Must be between 2 and 150");
            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null) {
                throw new ProductNotFoundException();
            }

            product.Name = command.name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = product.ImageFile;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
