using DG.Tweening;
using UnityEngine;

public class Generator : BaseUnit
{
    private void OnEnable()
    {
        if (_baseUnitSOData == null) return;
        if (!_baseUnitSOData.IsGenerator) return;

        InputHandler.Instance.OnClickAction += GenerateUnit;
    }

    private void OnDestroy()
    {
        InputHandler.Instance.OnClickAction -= GenerateUnit;
    }


    private void GenerateUnit(BaseUnit baseUnit)
    {
        if (baseUnit.gameObject == gameObject)
        {
            Vector2Int emptyTilePos = BoardManager.Instance.GetRandomEmptyTile();

            Tile tile = BoardManager.Instance.GetTileAtPosition(emptyTilePos);

            if (tile != null)
            {
                BaseUnit spawnUnit = _baseUnitSOData.GeneratorSOData.SelectObject();

                BaseUnit unit = BaseUnitObjectPool.GetObjectFromPool(spawnUnit);
                
                unit.Init(tile,spawnUnit.MergedUnitData(),BaseUnitObjectPool);
                
                unit.transform.position = transform.position;
                
                unit.gameObject.SetActive(true);
                
                tile.SetUnit(unit);
            }
        }
    }

    public override BaseUnitSOData MergedUnitData()
    {
        return _baseUnitSOData;
    }

    public override void SetCurrentSOData(BaseUnitSOData baseUnitSOData)
    {
        _baseUnitSOData = baseUnitSOData;
    }

    public override void SetCurrentTile(Tile tile)
    {
        CurrentTile = tile;

        transform.DOMove(tile.transform.position, .1f).SetEase(Ease.OutQuint);
    }

    public override BaseUnit MergedUnit()
    {
        return _baseUnitSOData.MergedUnit;
    }

    public override Tile GetCurrentTile()
    {
        return CurrentTile;
    }

    public override void PlayMergeAnim()
    {
        Animator.SetTrigger("Merge");
    }

    public override bool IsMaxLevel()
    {
        return _baseUnitSOData.IsMaxLevel;
    }

    public override string GetUnitName()
    {
        return _baseUnitSOData.UnitName;
    }
}