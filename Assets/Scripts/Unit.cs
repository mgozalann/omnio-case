using UnityEngine;

public class Unit : MonoBehaviour 
{
    [SerializeField] private Tile _currentTile;
    
    [SerializeField] private UnitSOData _unitData;

    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _unitData.UnitSprite;
    }

    public void Init(Tile tile)
    {
        SetCurrentTile(tile);
        _currentTile.SetUnit(this); // Tile sınıfında unit atama metodu olmalı
    }

    public string GetUnitName()
    {
        return _unitData.UnitName;
    }

    public Unit GetMergedUnit()
    {
        return _unitData.MergedUnit;
    }
    
    public Tile GetCurrentTile()
    {
        return _currentTile;
    }
    
    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
        transform.position = tile.transform.position;
    }
}