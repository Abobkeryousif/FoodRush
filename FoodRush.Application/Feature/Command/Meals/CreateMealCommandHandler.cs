
namespace FoodRush.Application.Feature.Command.Meals
{
    public record CreateMealCommand(MealDto mealDto) : IRequest<ApiResponse<MealDto>>;
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, ApiResponse<MealDto>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public CreateMealCommandHandler(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<MealDto>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _unitofwork.MealRepository.IsExist(m=> m.Name.ToLower() == request.mealDto.Name.ToLower() && m.RestaurantId == request.mealDto.RestaurantId);
            if (isExist)
                return new ApiResponse<MealDto>(HttpStatusCode.BadRequest, "This Meal Already Added");

            var meal = _mapper.Map<Meal>(request.mealDto);
            await _unitofwork.MealRepository.CreateAsync(meal);

            return new ApiResponse<MealDto>(HttpStatusCode.OK,"Success",request.mealDto);


            
        }
    }
}
