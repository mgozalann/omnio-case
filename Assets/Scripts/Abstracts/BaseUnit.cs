using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    [SerializeField] protected BaseUnitSOData _baseUnitSOData;
    protected Tile CurrentTile;
    protected SpriteRenderer SpriteRenderer;
    protected Animator Animator;
    protected BaseUnitObjectPool BaseUnitObjectPool;
    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_baseUnitSOData != null)
        {
            SpriteRenderer.sprite = _baseUnitSOData.UnitSprite;
        }
    }
    
    public void Init(Tile tile, BaseUnitSOData baseUnitSOData,BaseUnitObjectPool objectPool)
    {
        SetCurrentTile(tile);
        CurrentTile.SetUnit(this);
        if (_baseUnitSOData == null)
        {
            _baseUnitSOData = baseUnitSOData;
            SpriteRenderer.sprite = _baseUnitSOData.UnitSprite;
            BaseUnitObjectPool = objectPool;
        }
    }

    public abstract void SetCurrentSOData(BaseUnitSOData baseUnitSOData);
    public abstract void SetCurrentTile(Tile tile);
    public abstract BaseUnit MergedUnit();
    public abstract Tile GetCurrentTile();
    public abstract BaseUnitSOData GetBaseUnitData();
    public abstract void PlayMergeAnim();
    public abstract bool IsMaxLevel();
    public abstract string GetUnitName();

}