using Domain;

namespace Data.Entities;

public class PortfolioItemEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PortfolioItemType Type { get; set; }

    public int UnitId { get; set; }
    public PortfolioItemUnitEntity Unit { get; set; }
}
