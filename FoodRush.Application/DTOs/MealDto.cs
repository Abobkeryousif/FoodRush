

namespace FoodRush.Application.DTOs
{
    public class updateMealDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int RestaurantId { get; set; }


    }

    public class GetMealDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public RestaurantDto Restaurant { get; set; }

    }

    public class AddMealDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int RestaurantId { get; set; }

        public IFormFileCollection Photos { get; set; }

    }

    public record PatchMealAvailabilityDto
    {
        public bool IsAvailable { get; set; }
    }


}
