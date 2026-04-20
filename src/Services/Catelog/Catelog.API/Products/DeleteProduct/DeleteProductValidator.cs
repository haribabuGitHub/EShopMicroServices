using FluentValidation;

namespace Catelog.API.Products.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }
}
