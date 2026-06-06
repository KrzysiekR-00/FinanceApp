using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<PortfolioItemUnitEntity> PortfolioItemUnits => Set<PortfolioItemUnitEntity>();
    public DbSet<PortfolioItemEntity> PortfolioItems => Set<PortfolioItemEntity>();
    public DbSet<SettingEntity> Settings => Set<SettingEntity>();
    public DbSet<ExchangeRateSnapshotEntity> ExchangeRateSnapshots => Set<ExchangeRateSnapshotEntity>();
    public DbSet<PortfolioItemSnapshotEntity> PortfolioItemSnapshots => Set<PortfolioItemSnapshotEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}