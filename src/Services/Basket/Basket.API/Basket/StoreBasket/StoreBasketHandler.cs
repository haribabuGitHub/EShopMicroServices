

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

    public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart shoppingCart = command.ShoppingCart;

            await basketRepository.StoreBasket(shoppingCart);
            // Here you would typically save the shopping cart to a database or cache

            // For demonstration purposes, we'll just return a successful result
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}
