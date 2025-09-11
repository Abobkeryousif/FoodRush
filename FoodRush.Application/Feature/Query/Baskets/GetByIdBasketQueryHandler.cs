
namespace FoodRush.Application.Feature.Query.Baskets
{
    public record GetByIdBasketQuery(string id) : IRequest<ApiResponse<Basket>>;
    public class GetByIdBasketQueryHandler : IRequestHandler<GetByIdBasketQuery, ApiResponse<Basket>>
    {
        private readonly IUnitofwork _unitofwork;
        public GetByIdBasketQueryHandler(IUnitofwork unitofwork)=>
        
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<Basket>> Handle(GetByIdBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _unitofwork.BasketRepository.GetBasketAsync(request.id);

            if (basket == null)
                return new ApiResponse<Basket>(HttpStatusCode.NotFound,"Not Found Any Basket");

            return new ApiResponse<Basket>(HttpStatusCode.OK,"Success",basket);
                    
        }
    }
}
