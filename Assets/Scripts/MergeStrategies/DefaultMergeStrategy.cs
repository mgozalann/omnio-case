using UnityEngine;

public class DefaultMergeStrategy : IMergeStrategy
{
    private bool _canMerge;
    public void Merge(BaseUnit baseUnit, Tile tile)
    {
        SetCanMerge(false);
        
        if (tile.IsEmpty())
        {

            baseUnit.SetCurrentTile(baseUnit.GetCurrentTile());
            baseUnit.GetCurrentTile().SetUnit(baseUnit);
        }
        else
        {

            BaseUnit currentUnit = tile.GetUnit();

            if (tile.GetUnit().GetUnitName() == baseUnit.GetUnitName() && !baseUnit.IsMaxLevel())
            {
                
                SetCanMerge(true);
            }
            else
            {
                currentUnit.SetCurrentTile(baseUnit.GetCurrentTile());
                baseUnit.GetCurrentTile().SetUnit(currentUnit);

                tile.SetUnit(baseUnit);
                baseUnit.SetCurrentTile(tile);
                
            }
        }
    }

    private bool SetCanMerge(bool tr)
    {
        return _canMerge = tr;
    }
    public bool CanMerge()
    {
        return _canMerge;
    }

}