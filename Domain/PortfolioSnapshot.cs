namespace Domain;

public class PortfolioSnapshot
{
    public required DateOnly Date { get; set; }
    public required ExchangeRateSnapshot[] ExchangeRates { get; set; }
    public required PortfolioItemSnapshot[] PortfolioItems { get; set; }
}
