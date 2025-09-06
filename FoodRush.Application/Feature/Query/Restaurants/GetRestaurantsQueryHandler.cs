namespace FoodRush.Application.Feature.Query.Restaurants
{
    public record GetRestaurantsQuery : IRequest<ApiResponse<List<RestaurantDto>>>;
    public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, ApiResponse<List<RestaurantDto>>>
    {
        private readonly IUnitofwork _unitofwork;

        public GetRestaurantsQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        

        async Task<ApiResponse<List<RestaurantDto>>> IRequestHandler<GetRestaurantsQuery, ApiResponse<List<RestaurantDto>>>.Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitofwork.RestaurantRepository.GetAllAsync();
            if (restaurants.Count == 0)
                return new ApiResponse<List<RestaurantDto>>(HttpStatusCode.NotFound,"Not Found Any Restaurant");

            var restaurantDto = restaurants.Select(r=> new RestaurantDto
            {
                Name = r.Name,
                Address = r.Address,
                phoneNumber = r.phoneNumber,
                isOpen = r.isOpen,
                Rating = r.Rating,
            }).ToList();

            return new ApiResponse<List<RestaurantDto>>(HttpStatusCode.OK,"Success",restaurantDto);
        }
    }
}
