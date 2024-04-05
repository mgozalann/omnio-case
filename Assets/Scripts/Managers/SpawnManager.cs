using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private BaseUnit[] _baseUnitPrefabs;
    [SerializeField] private BaseUnitObjectPool _baseUnitObjectPool;

    public SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
            return saveData;
        }
        else
        {

            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
    
    private void Start()
    {
        LoadSavedGame();
    }

    private void LoadSavedGame()
    {
        var saveData = LoadGame();
        if (saveData != null)
        {
            foreach (var saveDataItem in saveData.SaveDataItems)
            {
                Vector2Int tilePos = saveDataItem.TilePos;
                
                BaseUnitSOData baseUnitSO = saveDataItem._baseUnitSOData;

                BaseUnit baseUnit = _baseUnitObjectPool.GetObjectFromPool(baseUnitSO.BaseUnit);

                Tile tile = BoardManager.Instance.GetTileAtPosition(tilePos);
                
                baseUnit.transform.position = tile.transform.position;

                baseUnit.Init(tile, baseUnitSO, _baseUnitObjectPool);
                
                baseUnit.gameObject.SetActive(true);
                
                tile.SetUnit(baseUnit);
            }
        }
        else
        {
            StartCoroutine(SpawnUnits());
        }
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
            BaseUnit unit = Instantiate(_baseUnitPrefabs[index], targetTile.transform.position, Quaternion.identity);
            unit.Init(targetTile,null,null); //gerekirse koyarız
        }
    }

}