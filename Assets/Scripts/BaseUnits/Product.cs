using DG.Tweening;

public class Product : BaseUnit
{

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

    public override BaseUnitSOData GetBaseUnitData()
    {
        return _baseUnitSOData;
    }

    public override bool IsMaxLevel()
    {
        return _baseUnitSOData.IsMaxLevel;
    }

    public override void SetCurrentSOData(BaseUnitSOData baseUnitSOData)
    {
        _baseUnitSOData = baseUnitSOData;
    }
    public override string GetUnitName()
    {
        return _baseUnitSOData.UnitName;
    }
    
    public override void PlayMergeAnim()
    {
        Animator.SetTrigger("Merge");
    }
}