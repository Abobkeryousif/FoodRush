

namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly ISender _sender;
        public userController(ISender sender)=>
            _sender = sender;

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserDto userDto) =>
            Ok(await _sender.Send(new RegisterUserCommand(userDto)));

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await _sender.Send(new GetUsersQuery()));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id) =>
            Ok(await _sender.Send(new GetUserByIdQuery(id)));


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id,UserDto userDto) =>
            Ok(await _sender.Send(new UpdateUserCommand(id,userDto)));

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateUserEmail(int id,UpdateUserEmailDto userDto) =>
            Ok(await _sender.Send(new PatchUserEmailCommand(id,userDto)));


       [HttpPatch("{id:int}/profile-picture")]
      public async Task<IActionResult> PatchProfilePicture(int id,[FromForm] UpdateUserProfilePictureDto dto,[FromServices] IWebHostEnvironment env)
        {
            return Ok(await _sender.Send(new PatchUserProfilePictureCommand(id, dto.File, env.WebRootPath)));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id) =>
            Ok(await _sender.Send(new DeleteUserCommand(id)));

       
    }
}
