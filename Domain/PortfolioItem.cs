namespace Domain;

public class PortfolioItem
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required PortfolioItemType Type { get; set; }
    public required PortfolioItemUnit Unit { get; set; }
}
