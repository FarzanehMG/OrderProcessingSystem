using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Persistence.Configurations;

internal sealed class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItemEntity>
{
    public void Configure(EntityTypeBuilder<InventoryItemEntity> builder)
    {
        builder.ToTable("InventoryItems");

        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductId)
            .ValueGeneratedNever();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        var seedDate = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        builder.HasData(
            new { ProductId = 1L, Quantity = 100, CreatedDate = seedDate, CreatedBy = "system" },
            new { ProductId = 2L, Quantity = 5, CreatedDate = seedDate, CreatedBy = "system" },
            new { ProductId = 3L, Quantity = 0, CreatedDate = seedDate, CreatedBy = "system" });
    }
}