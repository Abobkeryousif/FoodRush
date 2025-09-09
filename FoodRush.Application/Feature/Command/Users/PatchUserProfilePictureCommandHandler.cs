
namespace FoodRush.Application.Feature.Command.Users
{
    public record PatchUserProfilePictureCommand(int Id, IFormFile File, string WebRootPath)
    : IRequest<ApiResponse<string>>;

    public class PatchUserProfilePictureCommandHandler : IRequestHandler<PatchUserProfilePictureCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitOfWork;


        public PatchUserProfilePictureCommandHandler(IUnitofwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<string>> Handle(PatchUserProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (user == null)
                return new ApiResponse<string>(HttpStatusCode.NotFound, $"User not found with ID: {request.Id}");

            if (request.File == null || request.File.Length == 0)
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "Invalid file");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
            var folderPath = Path.Combine(request.WebRootPath, "profile-pictures");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            user.ProfileImageUrl = $"/profile-pictures/{fileName}";
            await _unitOfWork.UserRepository.UpdateAsync(user);

            return new ApiResponse<string>(HttpStatusCode.OK,"Profile picture updated successfully",user.ProfileImageUrl);
        }
    }
}
