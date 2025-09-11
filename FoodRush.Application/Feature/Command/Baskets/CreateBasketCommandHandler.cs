

namespace FoodRush.Application.Feature.Command.Baskets
{
    public record CreateBasketCommand(Basket basket) : IRequest<ApiResponse<Basket>>;
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, ApiResponse<Basket>>
    {
        private readonly IUnitofwork _unitofwork;

        public CreateBasketCommandHandler(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ApiResponse<Basket>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var customerBasket = await _unitofwork.BasketRepository.UpdateBasketAsync(request.basket);
            return new ApiResponse<Basket>(HttpStatusCode.OK,"Success",customerBasket);
        }
    }
}
