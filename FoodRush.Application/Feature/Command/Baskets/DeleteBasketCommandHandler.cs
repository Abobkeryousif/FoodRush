

namespace FoodRush.Application.Feature.Command.Baskets
{
    public record DeleteBasketCommand(string id) : IRequest<ApiResponse<string>>;
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, ApiResponse<string>>
    {
        private readonly IUnitofwork _unitofwork;
        public DeleteBasketCommandHandler(IUnitofwork unitofwork)=>
            _unitofwork = unitofwork;
        
        public async Task<ApiResponse<string>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _unitofwork.BasketRepository.DeleteBasketAsync(request.id);

            return basket ? new ApiResponse<string>(HttpStatusCode.OK,"Success","Complete Deleted Item") : 
                new ApiResponse<string>(HttpStatusCode.BadRequest,"Something Wrong");
        }
    }
}
