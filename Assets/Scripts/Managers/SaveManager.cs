using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    private void OnApplicationQuit()
    {
        SaveGameDatas();
    }

    private void SaveGameDatas()
    {
        List<Tile> allTiles = BoardManager.Instance.GetOccupiedTiles();

        List<SaveDataItem> saveDataItems = new List<SaveDataItem>();
        
        foreach (Tile tile in allTiles)
        {
            Vector2Int tilePos = tile.GetTilePos();
            
            BaseUnitSOData baseUnitSOData = tile.GetUnit().GetBaseUnitData(); 
            
            SaveDataItem saveDataItem = new SaveDataItem(tilePos, baseUnitSOData);
            
            saveDataItems.Add(saveDataItem);
        }

        SaveData saveData = new SaveData(saveDataItems);
        
        string jsonData = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", jsonData);
    }
}