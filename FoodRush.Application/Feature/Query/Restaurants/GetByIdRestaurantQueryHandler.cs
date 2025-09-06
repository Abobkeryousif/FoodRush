
using FoodRush.Domain.Entites;

namespace FoodRush.Application.Feature.Query.Restaurants
{
    public record GetByIdRestaurantQuery(int id) : IRequest<ApiResponse<Restaurant>>;
    public class GetByIdRestaurantQueryHandler : IRequestHandler<GetByIdRestaurantQuery, ApiResponse<Restaurant>>
    {
        private readonly IUnitofwork _unitofwork;
        public GetByIdRestaurantQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
    public async Task<ApiResponse<Restaurant>> Handle(GetByIdRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitofwork.RestaurantRepository.FirstOrDefaultAsync(r=> r.Id == request.id);
            if (restaurant == null)
                return new ApiResponse<Restaurant>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            return new ApiResponse<Restaurant>(HttpStatusCode.OK,"Success",restaurant);
        }
    }
}
