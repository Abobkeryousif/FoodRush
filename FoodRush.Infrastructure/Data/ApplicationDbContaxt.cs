namespace FoodRush.Infrastructure.Data
{
    public class ApplicationDbContaxt : DbContext
    {
        public ApplicationDbContaxt(DbContextOptions<ApplicationDbContaxt> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
