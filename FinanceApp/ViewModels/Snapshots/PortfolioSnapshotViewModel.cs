namespace FinanceApp.ViewModels.Snapshots;

internal class PortfolioSnapshotViewModel
{
    public required DateOnly Date { get; set; }
    public required ExchangeRateSnapshotViewModel[] ExchangeRates { get; set; }
    public required PortfolioItemSnapshotViewModel[] PortfolioItems { get; set; }
}
