
namespace FoodRush.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Message = "Invalid model state",
                    Errors = context.ModelState.Values.SelectMany(v=> v.Errors).Select(em=> em.ErrorMessage)
                });
            }
        }
    }
}
