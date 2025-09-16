
namespace FoodRush.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Meal, AddMealDto>().ReverseMap();
            CreateMap<Meal, AddMealDto>().ReverseMap().ForMember(p=> p.Photos , o=> o.Ignore());
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<Review, GetAndUpdateReviewDto>().ReverseMap();
        }
    }
}
