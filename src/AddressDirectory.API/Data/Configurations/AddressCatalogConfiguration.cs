namespace AddressDirectory.API.Data.Configurations
{
    public class AddressCatalogConfiguration : IEntityTypeConfiguration<AddressCatalog>
    {
        public void Configure(EntityTypeBuilder<AddressCatalog> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.Value).IsRequired();

            builder.Property(a => a.StatusId).IsRequired();

            builder.Property(a => a.CUserId).IsRequired();

            builder.Property(a => a.UUserId).IsRequired();

            // Constraints
            builder.ToTable(t => t.HasCheckConstraint("CK_AddressCatalog_StatusId", "\"StatusId\" IN (0, 1)"));
        }
    }
}
