namespace Domain;

internal class ExchangeRateSnapshot
{
    public required int Id { get; set; }
    public required int AssetUnitId { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal Value { get; set; }

}
