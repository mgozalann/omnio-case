using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Units/Unit")]
public class UnitSOData : ScriptableObject
{
    public string UnitName;
    
    public Sprite UnitSprite;

    public bool IsMaxLevel;
    
    public Unit MergedUnit;
}