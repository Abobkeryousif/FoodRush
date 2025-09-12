

namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class OrderItemConfigruation : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
