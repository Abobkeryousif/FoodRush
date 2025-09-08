namespace FoodRush.Infrastructure.Data
{
    public class ApplicationDbContaxt : DbContext
    {
        public ApplicationDbContaxt(DbContextOptions<ApplicationDbContaxt> options) : base(options)
        {
        }

        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
