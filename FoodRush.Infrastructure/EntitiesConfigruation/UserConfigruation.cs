

namespace FoodRush.Infrastructure.EntitiesConfigruation
{
    public class UserConfigruation : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u=> u.firstName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u=> u.lastName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u=> u.Email)
                .IsRequired()
                .HasMaxLength(55);

            builder.Property(u=> u.Phone)
                .HasMaxLength(25);

            builder.Property(u => u.City)
                .HasMaxLength(30);

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
