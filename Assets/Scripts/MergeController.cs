using System;
using UnityEngine;

public class MergeController : MonoBehaviour
{

    private void Start()
    {
        InputController.Instance.OnDropAction+=TryMerge;
    }

    private void OnDisable()
    {
        InputController.Instance.OnDropAction-=TryMerge;
    }

    private void TryMerge(Unit unit, Tile tile)
    {
        if (tile.IsEmpty())
        {
            unit.SetCurrentTile(unit.GetCurrentTile());
            unit.GetCurrentTile().SetUnit(unit);
        }
        else
        {
            if (tile.GetUnit().GetUnitName() == unit.GetUnitName())
            {
                Destroy(unit.gameObject);
                Destroy(tile.GetUnit().gameObject);

                Unit mergedUnit = Instantiate(unit.GetMergedUnit(), tile.transform.position, Quaternion.identity);
                
                mergedUnit.Init(tile);
                tile.SetUnit(mergedUnit);
            }
            else
            {
                Unit currentUnit = tile.GetUnit();
                    
                currentUnit.SetCurrentTile(unit.GetCurrentTile());
                unit.GetCurrentTile().SetUnit(currentUnit);
                    
                tile.SetUnit(unit);
                unit.SetCurrentTile(tile);
            }
        }
    }
}