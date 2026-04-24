

using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x=>x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.ShoppingCart).NotNull();
            RuleFor(x => x.ShoppingCart.Items).NotEmpty();
        }
    }

    public class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountPro) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.ShoppingCart, cancellationToken);
            await basketRepository.StoreBasket(command.ShoppingCart);
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        { 
            foreach (var item in shoppingCart.Items)
            {
                var discount = await discountPro.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
                item.Price -= discount.Amount;
            }
        }
    }
}
