using UnityEngine;

public class MergeController : MonoBehaviour
{
    private IMergeStrategy _mergeStrategy = new DefaultMergeStrategy();

    [SerializeField] private BaseUnitObjectPool _baseUnitObjectPool;

    private void OnEnable()
    {
        InputHandler.Instance.OnDropAction += TryMerge;
    }

    private void OnDestroy()
    {
        InputHandler.Instance.OnDropAction -= TryMerge;
    }

    private void TryMerge(BaseUnit baseUnit, Tile tile)
    {
        _mergeStrategy.Merge(baseUnit, tile);

        if (_mergeStrategy.CanMerge()) 
        {

            BaseUnit mergedUnit = baseUnit.MergedUnit();

            baseUnit.SetCurrentSOData(null);
            tile.GetUnit().SetCurrentSOData(null);

            _baseUnitObjectPool.ReturnObjectToPool(baseUnit);
            _baseUnitObjectPool.ReturnObjectToPool(tile.GetUnit());

            BaseUnit baseUnitInstance = _baseUnitObjectPool.GetObjectFromPool(mergedUnit);

            baseUnitInstance.transform.position = tile.transform.position;

            baseUnitInstance.Init(tile, mergedUnit.GetBaseUnitData(), _baseUnitObjectPool);

            baseUnitInstance.gameObject.SetActive(true);

            baseUnitInstance.PlayMergeAnim();

            tile.SetUnit(baseUnitInstance);
        }
    }
}