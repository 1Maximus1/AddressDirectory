namespace AddressDirectory.API.Data
{
    public class AddressContext : DbContext
    {
        public DbSet<AddressCatalog> Addresses { get; set; } = default!;

        public AddressContext(DbContextOptions<AddressContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressCatalogConfiguration());
        }
    }
}
