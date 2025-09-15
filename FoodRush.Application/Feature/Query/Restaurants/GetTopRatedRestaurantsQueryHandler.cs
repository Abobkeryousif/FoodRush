

namespace FoodRush.Application.Feature.Query.Restaurants
{
    public record GetTopRatedRestaurantsQuery(int count) : IRequest<ApiResponse<List<RestaurantDto>>>;
    public class GetTopRatedRestaurantsQueryHandler : IRequestHandler<GetTopRatedRestaurantsQuery, ApiResponse<List<RestaurantDto>>>
    {
        private readonly IUnitofwork _unitofwork;
        public GetTopRatedRestaurantsQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<List<RestaurantDto>>> Handle(GetTopRatedRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitofwork.RestaurantRepository.GetAllAsync();

            if (restaurants == null || request.count == 0)
                return new ApiResponse<List<RestaurantDto>>(HttpStatusCode.NotFound, "No Restaurants Found");

            var topRatedRestaurants = restaurants.OrderByDescending(rt => rt.Rating)
                .Take(request.count)
                .Select(r => new RestaurantDto
                {
                    Name = r.Name,
                    Address = r.Address,
                    phoneNumber = r.phoneNumber,
                    isOpen = r.isOpen,
                    Rating = r.Rating
                }).ToList();

            return new ApiResponse<List<RestaurantDto>>(HttpStatusCode.OK, "Success", topRatedRestaurants);
        }
    }
}
