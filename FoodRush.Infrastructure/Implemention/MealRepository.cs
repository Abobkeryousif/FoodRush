
namespace FoodRush.Infrastructure.Implemention
{
    public class MealRepository : GeneraicRepository<Meal>, IMealRepository
    {
        private readonly ApplicationDbContaxt _contaxt;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public MealRepository(ApplicationDbContaxt context, IMapper mapper, IPhotoService photoService) : base(context)
        {
            _contaxt = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<bool> AddAsync(AddMealDto mealDto)
        {
            if (mealDto == null) return false;

            var Meal = _mapper.Map<Meal>(mealDto);
            await _contaxt.Meals.AddAsync(Meal);
            await _contaxt.SaveChangesAsync();

            var imagePaths = await _photoService.AddPhotoAsync(mealDto.Photos, Meal.Name);

            var images = imagePaths.Select(path => new Photo
            {

                photoName = path,
                mealId = Meal.mealId
            }).ToList();
            return true;
        }

        public Task<List<GetMealDto>> GetAllMealAsyncV1()
        {
            var result = _contaxt.Meals.Select(m => new GetMealDto
            {
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                IsAvailable = m.IsAvailable,
                Restaurant = new GetRestaurantDto
                {
                    Name = m.Restaurant.Name,
                    Address = m.Restaurant.Address,
                    phoneNumber = m.Restaurant.phoneNumber,
                    Rating = m.Restaurant.Rating
                }
            })
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<List<GetMealDto>> GetAllMealAsyncV2(QueryParameters parameters)
        {
            var query = _contaxt.Meals
                .Include(m => m.Restaurant)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(parameters.FilterOn) && !string.IsNullOrWhiteSpace(parameters.FilterQuery))
            {
                if (parameters.FilterOn.Equals("MealName", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(m => m.Name.Contains(parameters.FilterQuery));

                else if (parameters.FilterOn.Equals("RestaurantName", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(m => m.Restaurant.Name.Contains(parameters.FilterQuery));
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                if (parameters.SortBy.Equals("MealName", StringComparison.OrdinalIgnoreCase))
                    query = parameters.IsAscending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name);

                else if (parameters.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                    query = parameters.IsAscending ? query.OrderBy(m => m.Price) : query.OrderByDescending(m => m.Price);

                else if (parameters.SortBy.Equals("RestaurantName", StringComparison.OrdinalIgnoreCase))
                    query = parameters.IsAscending ? query.OrderBy(m => m.Restaurant.Name) : query.OrderByDescending(m => m.Restaurant.Name);
            }


            // Pagination with Default & MaxPageSize
            const int maxPageSize = 20;
            const int defaultPageSize = 10;

            var pageSize = parameters.PageSize <= 0 ? defaultPageSize :
                (parameters.PageSize > maxPageSize ? maxPageSize : parameters.PageSize);

            var pageNumber = parameters.PageNumber <= 0 ? 1 : parameters.PageNumber;

            var skip = (pageNumber - 1) * pageSize;

            var result = await query
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .Select(m => new GetMealDto
                {
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    IsAvailable = m.IsAvailable,
                    Restaurant = new GetRestaurantDto
                    {
                        Name = m.Restaurant.Name,
                        Address = m.Restaurant.Address,
                        phoneNumber = m.Restaurant.phoneNumber,
                        Rating = m.Restaurant.Rating
                    }
                })
                .ToListAsync();

            return result;
        }

    }
}

