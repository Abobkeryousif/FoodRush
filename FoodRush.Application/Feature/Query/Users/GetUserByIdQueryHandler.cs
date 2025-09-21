

namespace FoodRush.Application.Feature.Query.Users
{
    public record GetUserByIdQuery(int id) : IRequest<ApiResponse<GetUserDto>>;
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<GetUserDto>>
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitofwork.UserRepository.GetByIdAsync(request.id);
            if (user == null)
                return new ApiResponse<GetUserDto>(HttpStatusCode.NotFound,$"Not Found With ID: {request.id}");

            var userDto = _mapper.Map<GetUserDto>(user);
            return new ApiResponse<GetUserDto>(HttpStatusCode.OK,"Success",userDto);
        }
    }
}
