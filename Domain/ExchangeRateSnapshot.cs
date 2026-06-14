namespace Domain;

public class ExchangeRateSnapshot
{
    public required int Id { get; set; }
    public required PortfolioItemUnit PortfolioItemUnit { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal Value { get; set; }
}
