using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataItem
{
    public Vector2Int TilePos;
    public BaseUnitSOData _baseUnitSOData;

    public SaveDataItem(Vector2Int tilePos, BaseUnitSOData baseUnitSOData)
    {
        this.TilePos = tilePos;
        this._baseUnitSOData = baseUnitSOData;
    }
}

[System.Serializable]
public class SaveData
{
    [SerializeField] private List<SaveDataItem> _saveDataItems;

    public List<SaveDataItem> SaveDataItems => _saveDataItems;

    public SaveData(List<SaveDataItem> saveDataItems)
    {
        _saveDataItems = saveDataItems ?? new List<SaveDataItem>();
    }
}