
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    
    public class userController : ControllerBase
    {
        private readonly ISender _sender;
        public userController(ISender sender)=>
            _sender = sender;

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegister(UserDto userDto)
        {
            return Ok(await _sender.Send(new RegisterUserCommand(userDto)));
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> GetAllUsers()
        {

            return Ok(await _sender.Send(new GetUsersQuery()));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        public async Task<IActionResult> GetUserById(int id)
        {

            return Ok(await _sender.Send(new GetUserByIdQuery(id)));
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        public async Task<IActionResult> UpdateUser(int id,UserDto userDto) =>
            Ok(await _sender.Send(new UpdateUserCommand(id,userDto)));

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        public async Task<IActionResult> UpdateUserEmail(int id,UpdateUserEmailDto userDto) =>
            Ok(await _sender.Send(new PatchUserEmailCommand(id,userDto)));


       [HttpPatch("{id:int}/profile-picture")]
        [Authorize(Roles = "USER,ADMIN,SUPERADMIN")]
        public async Task<IActionResult> PatchProfilePicture(int id,[FromForm] UpdateUserProfilePictureDto dto,[FromServices] IWebHostEnvironment env)
        {
            return Ok(await _sender.Send(new PatchUserProfilePictureCommand(id, dto.File, env.WebRootPath)));
        }

        [HttpPatch("{userId:int}/user-role")]
        [Authorize(Roles = "SUPERADMIN")]
        public async Task<IActionResult> PatchUserRole(int userId,[FromBody]UserRoleDto Role)
        {
            return Ok(await _sender.Send(new PatchUserRoleCommand(userId,Role)));
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "SUPERADMIN")]
        public async Task<IActionResult> DeleteUser(int id) =>
            Ok(await _sender.Send(new DeleteUserCommand(id)));

        }
    }

  
