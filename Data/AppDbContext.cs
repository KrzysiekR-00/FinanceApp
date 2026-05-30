using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<PortfolioItemUnitEntity> PortfolioItemUnits => Set<PortfolioItemUnitEntity>();
    public DbSet<SettingEntity> Settings => Set<SettingEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}