namespace NorthWind.EFCore.Repositories.Configuration;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.Id)
            .HasMaxLength(5)
            .IsFixedLength();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(c => c.CurrentBalance)
            .HasPrecision(8, 2);

        builder.HasData(
            new Customer
            {
                Id = "ALFKI",
                Name = "Alfreds Futter Kiste",
                CurrentBalance = 0
            },
            new Customer
            {
                Id = "ANATR",
                Name = "Ana Trujillo Emparedados y helados",
                CurrentBalance = 0
            },
            new Customer
            {
                Id = "ANTON",
                Name = "Antonio Moreno Taqueria",
                CurrentBalance = 100
            });
    }
}
