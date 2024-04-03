using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    public bool IsEmpty()
    {
        return _unit == null;
    }
    public void SetUnit(Unit unit)
    {
        _unit = unit;
    }

    public Unit GetUnit()
    {
        return _unit;
    }
    
    public void RemoveUnit()
    {
        _unit = null;
    }
}
