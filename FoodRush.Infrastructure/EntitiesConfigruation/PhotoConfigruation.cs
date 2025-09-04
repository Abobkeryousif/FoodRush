namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class PhotoConfigruation : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasOne(m => m.Meal)
                .WithMany(p => p.Photos)
                    .HasForeignKey(p => p.mealId);
        }
    }
}
