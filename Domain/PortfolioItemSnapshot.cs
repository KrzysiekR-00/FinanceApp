namespace Domain;

public class PortfolioItemSnapshot
{
    public required int Id { get; set; }
    public required PortfolioItem PortfolioItem { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal Quantity { get; set; }
}
