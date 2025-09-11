
namespace FoodRush.Infrastructure.Implemention
{
    public class Unitofwork : IUnitofwork
    {
        private readonly ApplicationDbContaxt _contaxt;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IDatabase _database;
        public Unitofwork(ApplicationDbContaxt contaxt, IPhotoService photoService, IMapper mapper,IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            _contaxt = contaxt;
            RestaurantRepository = new RestaurantRepository(_contaxt);
            MealRepository = new MealRepository(_contaxt,mapper,photoService);
            _photoService = photoService;
            _mapper = mapper;
            UserRepository = new UserRepository(_contaxt);
            OtpRepository = new OtpRepository(_contaxt);
            RefreshTokenRepository = new RefreshTokenRepository(_contaxt);
            VerficiationRepository = new VerficiationRepository(_contaxt);
            BasketRepository = new BasketRepository(redis);
        }
        public IRestaurantRepository RestaurantRepository { get; }

        public IMealRepository MealRepository {  get; }

        public IUserRepository UserRepository { get; }

        public IOtpRepository OtpRepository { get; }

        public IRefreshTokenRepository RefreshTokenRepository {  get; }

        public IVerficiationRepository VerficiationRepository { get; }

        public IBasketRepository BasketRepository {  get; }
    }
}
