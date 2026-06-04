using Domain;

namespace FinanceApp.ViewModels.Snapshots;

internal class ExchangeRateSnapshotViewModel
{
    public required PortfolioItemUnit PortfolioItemUnit { get; set; }
    public required decimal Value { get; set; }
}
