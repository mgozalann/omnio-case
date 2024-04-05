using UnityEngine;

public class Tile : MonoBehaviour
{
    private BaseUnit _baseUnit;

    private int[,] _index;
    
    public void Init(int xIndex,int yIndex)
    {
        _index = new int[xIndex,yIndex];
    }
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

    public int[,] GetTileIndex()
    {
        return _index;
    }

    public Vector2Int GetTilePos()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        
        return new Vector2Int(x, y);
    }
}