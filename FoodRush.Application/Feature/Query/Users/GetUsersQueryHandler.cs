
namespace FoodRush.Application.Feature.Query.Users
{
    public record GetUsersQuery : IRequest<ApiResponse<List<GetUserDto>>>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<List<GetUserDto>>>
    {
        private readonly IUnitofwork _unitofwork;
        public GetUsersQueryHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<List<GetUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitofwork.UserRepository.GetAllAsync();
            if (users.Count == 0)
                return new ApiResponse<List<GetUserDto>>(HttpStatusCode.NotFound,"Not Found Any User");

            var userDto = users.Select(u=> new GetUserDto
            {
                firstName = u.firstName,
                lastName = u.lastName,
                Phone = u.Phone,
                Email = u.Email,
                City = u.City,
                Address = u.Address,
                BirthDate = u.BirthDate,
            }).ToList();

            return new ApiResponse<List<GetUserDto>>(HttpStatusCode.OK,"Success",userDto);
        }

    }
}
