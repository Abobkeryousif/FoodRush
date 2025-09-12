

namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class DeliveryConfigruation : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasData(new Delivery { Id = 1, CompanyName = "Noon", DeliveryTime = "Two Days", Description = "Best Delivery Company And Fast", Price = 22 });
            builder.HasData(new Delivery { Id = 2, CompanyName = "Jahez", DeliveryTime = "1 Day", Description = "Best Delivery Company And Fast", Price = 19 });

        }
    }
}
