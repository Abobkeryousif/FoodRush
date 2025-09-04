namespace FoodRush.Domain.Entites
{
    public class Photo
    {
        public int photoId { get; set; }
        public string photoName { get; set; }
        public int mealId { get; set; }
        public Meal Meal { get; set; }
    }
}
