using Domain;

namespace FinanceApp.ViewModels.Snapshots;

internal class PortfolioItemSnapshotViewModel
{
    public required PortfolioItem PortfolioItem { get; set; }
    public required decimal Quantity { get; set; }
    public required bool SkipUpdate { get; set; }
}
