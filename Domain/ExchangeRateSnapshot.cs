namespace Domain;

public class ExchangeRateSnapshot
{
    public required int Id { get; set; }
    public required int PortfolioItemUnitId { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal Value { get; set; }

}
