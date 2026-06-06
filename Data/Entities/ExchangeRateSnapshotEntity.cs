namespace Data.Entities;

public class ExchangeRateSnapshotEntity
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Value { get; set; }

    public int UnitId { get; set; }
    public PortfolioItemUnitEntity Unit { get; set; }
}
