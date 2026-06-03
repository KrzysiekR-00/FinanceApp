namespace Domain;

public class Snapshot
{
    public DateOnly Date { get; set; }
    public ExchangeRateSnapshot[] ExchangeRates { get; set; }
    public PortfolioItemSnapshot[] PortfolioItems { get; set; }
}
