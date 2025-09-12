
namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class OrderConfigruation : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(o => o.shippingAddress,
                n=> { n.WithOwner(); });

            builder.HasMany(o=> o.orderItems)
                .WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.status).HasConversion(os => os.ToString(), oe=> (Status)Enum.Parse(typeof(Status),oe));

            builder.Property(p => p.subTotal).HasColumnType("decimal(18,2)");
        }
    }
}
