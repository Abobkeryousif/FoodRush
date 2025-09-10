
namespace FoodRush.Infrastructure.Implemention
{
    public class Unitofwork : IUnitofwork
    {
        private readonly ApplicationDbContaxt _contaxt;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public Unitofwork(ApplicationDbContaxt contaxt, IPhotoService photoService, IMapper mapper)
        {
            _contaxt = contaxt;
            RestaurantRepository = new RestaurantRepository(_contaxt);
            MealRepository = new MealRepository(_contaxt,mapper,photoService);
            _photoService = photoService;
            _mapper = mapper;
            UserRepository = new UserRepository(_contaxt);
            OtpRepository = new OtpRepository(_contaxt);
            RefreshTokenRepository = new RefreshTokenRepository(_contaxt);
        }
        public IRestaurantRepository RestaurantRepository { get; }

        public IMealRepository MealRepository {  get; }

        public IUserRepository UserRepository { get; }

        public IOtpRepository OtpRepository { get; }

        public IRefreshTokenRepository RefreshTokenRepository {  get; }
    }
}
