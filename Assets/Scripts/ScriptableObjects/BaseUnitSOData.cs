using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Units/Unit")] 
public class BaseUnitSOData : ScriptableObject
{
    public string UnitName;
    
    public Sprite UnitSprite;

    public bool IsMaxLevel;
    
    public BaseUnit MergedUnit;
    public BaseUnitSOData MergedUnitData;

    public bool IsGenerator;
    
#if UNITY_EDITOR
    // Editor'da sadece IsGenerator true olduğunda GeneratorData alanını göstermek için özel bir fonksiyon
    public GeneratorSOData GeneratorSOData;
    
    [CustomEditor(typeof(BaseUnitSOData))]
    public class UnitSODataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("UnitName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("UnitSprite"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("IsMaxLevel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MergedUnit"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("IsGenerator"));

            BaseUnitSOData unitSOData = (BaseUnitSOData)target;
            if (unitSOData.IsGenerator)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("GeneratorSOData"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif 
}