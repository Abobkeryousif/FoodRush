namespace FoodRush.Application.Feature.Query.Restaurants
{
    public record GetRestaurantsQuery : IRequest<ApiResponse<List<GetRestaurantDto>>>;
    public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, ApiResponse<List<GetRestaurantDto>>>
    {
        private readonly IUnitofwork _unitofwork;

        public GetRestaurantsQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        

        async Task<ApiResponse<List<GetRestaurantDto>>> IRequestHandler<GetRestaurantsQuery, ApiResponse<List<GetRestaurantDto>>>.Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitofwork.RestaurantRepository.GetAllAsync();
            if (restaurants.Count == 0)
                return new ApiResponse<List<GetRestaurantDto>>(HttpStatusCode.NotFound,"Not Found Any Restaurant");

            var restaurantDto = restaurants.Select(r=> new GetRestaurantDto
            {
                Name = r.Name,
                Address = r.Address,
                phoneNumber = r.phoneNumber,
                isOpen = r.isOpen,
                Rating = r.Rating,
            }).ToList();

            return new ApiResponse<List<GetRestaurantDto>>(HttpStatusCode.OK,"Success",restaurantDto);
        }
    }
}
