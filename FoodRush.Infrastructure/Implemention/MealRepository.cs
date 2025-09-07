
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

        public async Task<List<GetMealDto>> GetAllMealAsync()
        {
            var result = await _contaxt.Meals
                .Include(m => m.Restaurant)
                .Select(m => new GetMealDto
                {
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    IsAvailable = m.IsAvailable,
                    Restaurant = new RestaurantDto
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

    }
}

