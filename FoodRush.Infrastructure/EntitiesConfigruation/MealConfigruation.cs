namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class MealConfigruation : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(m => m.mealId);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(m => m.Description)
                .HasMaxLength(120);

            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(r => r.Restaurant)
                .WithMany(m => m.Meals)
                .HasForeignKey(r => r.RestaurantId);
        }
    }
}
