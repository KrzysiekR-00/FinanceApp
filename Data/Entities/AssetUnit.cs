namespace Data.Entities;

public class AssetUnitEntity
{
    public required int Id { get; set; }
    public required string Symbol { get; set; }
    public required decimal UnitModifier { get; set; }
}
