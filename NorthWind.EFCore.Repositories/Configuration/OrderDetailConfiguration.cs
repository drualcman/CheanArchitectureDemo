namespace NorthWind.EFCore.Repositories.Configuration;

internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.ProductId });

        builder.Property(d => d.UnitPrice)
            .HasPrecision(8, 2);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(p => p.ProductId);
    }
}
