
namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class ReviewConfigruation : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Comment).HasMaxLength(350);
            builder.Property(r => r.Rating).IsRequired();
        }
    }
}
