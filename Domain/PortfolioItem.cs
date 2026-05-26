namespace Domain;

internal class PortfolioItem
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required AssetUnit Unit { get; set; }
}
