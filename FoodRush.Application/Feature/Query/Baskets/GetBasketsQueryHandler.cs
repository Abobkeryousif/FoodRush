

namespace FoodRush.Application.Feature.Query.Baskets
{
    public record GetBasketsQuery : IRequest<ApiResponse<List<Basket>>>;
    public class GetBasketsQueryHandler : IRequestHandler<GetBasketsQuery, ApiResponse<List<Basket>>>
    {
        private readonly IUnitofwork _unitofwork;
        public GetBasketsQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<List<Basket>>> Handle(GetBasketsQuery request, CancellationToken cancellationToken)
        {
            var baskets = await _unitofwork.BasketRepository.GetAllBasketAsync();

            if (baskets.Count == 0)
                return new ApiResponse<List<Basket>>(HttpStatusCode.NotFound,"Not Found Any Item");

            return new(HttpStatusCode.OK,"Success",baskets);
        }
    }
}
