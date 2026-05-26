namespace Domain;

internal class PortfolioItemSnapshot
{
    public required int Id { get; set; }
    public required int PortfolioItemId { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal Quantity { get; set; }

    public required decimal UnitModifier { get; set; }
    public required decimal UnitValue { get; set; }
    public decimal CalculatedValue => Quantity * UnitModifier * UnitValue;
}
