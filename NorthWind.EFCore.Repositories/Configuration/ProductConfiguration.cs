namespace NorthWind.EFCore.Repositories.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p=>p.Name)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(p => p.UnitPrice)
            .HasPrecision(8, 2);

        builder.HasData(
            new Product
            {
                Id = 1,
                Name = "Chai",
                UnitPrice = 35,
                UnitsInStock = 20,
                Discontinued = false
            },
            new Product
            {
                Id = 2,
                Name = "Chang",
                UnitPrice = 55,
                UnitsInStock = 0,
                Discontinued = false
            },
            new Product
            {
                Id = 3,
                Name = "Aniseed Syrup",
                UnitPrice = 65,
                UnitsInStock = 20,
                Discontinued = true
            },
            new Product
            {
                Id = 4,
                Name = "Chef Anton's Cajun Seasoning",
                UnitPrice = 75,
                UnitsInStock = 40,
                Discontinued = false
            });
    }
}
