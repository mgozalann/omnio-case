using UnityEngine;

public class Tile : MonoBehaviour
{
    private BaseUnit _baseUnit;

    public bool IsEmpty()
    {
        return _baseUnit == null;
    }
    public void SetUnit(BaseUnit baseUnit)
    {
        _baseUnit = baseUnit;
    }

    public BaseUnit GetUnit()
    {
        return _baseUnit;
    }
    
    public void RemoveUnit()
    {
        _baseUnit = null;
    }
}