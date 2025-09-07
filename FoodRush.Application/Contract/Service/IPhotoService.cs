
namespace FoodRush.Application.Contract.Service
{
    public interface IPhotoService
    {
        Task<List<string>> AddPhotoAsync(IFormFileCollection file, string src);
        void DeletePhotoAsync(string src);
    }
}
