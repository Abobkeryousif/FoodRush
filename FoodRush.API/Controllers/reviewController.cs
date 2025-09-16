
namespace FoodRush.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ReviewController : ControllerBase
    {
        private readonly ISender _sender;

        public ReviewController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            int userId = GetCurrentUserId();
            var result = await _sender.Send(new CreateReviewCommand(reviewDto, userId));
            return Ok(result);
        }

        [HttpGet("Restaurant/{restaurantId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsByRestaurantId(int restaurantId)
        {
            return Ok(await _sender.Send(new GetReviewsQuery(restaurantId)));
        }


        [HttpPut("{reviewId}")]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]

        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] GetAndUpdateReviewDto reviewDto)
        {
            return Ok(await _sender.Send(new UpdateReviewCommand(reviewId,reviewDto)));
        }

        [HttpDelete("{reviewId}")]
        [Authorize(Roles = "ADMIN,SUPERADMIN")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            return Ok(await _sender.Send(new DeleteReviewCommand(reviewId)));
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                throw new UnauthorizedAccessException("User Id not found or invalid token");

            return userId;
        }
    }
}
