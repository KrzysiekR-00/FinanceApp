namespace Data.Entities;

public class PortfolioItemSnapshotEntity
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Quantity { get; set; }

    public int PortfolioItemId { get; set; }
    public PortfolioItemEntity PortfolioItem { get; set; }
}
