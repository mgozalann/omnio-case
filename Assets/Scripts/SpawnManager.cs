using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Unit[] _unitPrefabs; 

    private void Start()
    {
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        yield return null;
        
        SpawnUnitAtSelectedTile(new Vector2Int(2,3),0);
        SpawnUnitAtSelectedTile(new Vector2Int(2,2),0);
        SpawnUnitAtSelectedTile(new Vector2Int(3,2),1);
        SpawnUnitAtSelectedTile(new Vector2Int(3,1),2);
        SpawnUnitAtSelectedTile(new Vector2Int(4,1),3);
    }

    private void SpawnUnitAtSelectedTile(Vector2Int spawnPos,int index)
    {
        Vector2Int targetPosition = spawnPos;
        Tile targetTile = BoardManager.Instance.GetTileAtPosition(targetPosition);

        if (targetTile != null)
        {
            Unit unit = Instantiate(_unitPrefabs[index], targetTile.transform.position, Quaternion.identity);
            unit.Init(targetTile);
        }
    }
}