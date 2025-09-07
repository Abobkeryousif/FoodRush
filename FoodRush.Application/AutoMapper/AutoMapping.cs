
namespace FoodRush.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Meal, MealDto>().ReverseMap();
        }
    }
}
