namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class RestaurantConfigruation : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(r => r.phoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.Address)
                .IsRequired()
                .HasMaxLength(200);

        }
    }
}
