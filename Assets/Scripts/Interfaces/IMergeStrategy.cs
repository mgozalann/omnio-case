public interface IMergeStrategy
{
    void Merge(BaseUnit baseUnit, Tile tile);
    bool CanMerge();
}