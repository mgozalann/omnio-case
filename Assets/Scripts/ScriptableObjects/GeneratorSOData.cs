using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewGeneratorData", menuName = "Generators/GeneratorData")]
public class GeneratorSOData : ScriptableObject
{
    [System.Serializable]
    public class ProductProbability
    {
        [FormerlySerializedAs("_product")] [FormerlySerializedAs("Unit")] public BaseUnit _baseUnit;
        [Range(0f, 100f)]
        public float Probability;
    }

    [SerializeField] private ProductProbability[] _productProbabilities;
    
    public BaseUnit SelectObject()
    {
        float totalProbability = 0f;
        foreach (var objProb in _productProbabilities)
        {
            totalProbability += objProb.Probability;
        }
    
        float randomValue = Random.Range(0f, totalProbability);
    
        float cumulativeProbability = 0f;
        foreach (var objProb in _productProbabilities)
        {
            cumulativeProbability += objProb.Probability;
            if (randomValue <= cumulativeProbability)
            {
                return objProb._baseUnit;
            }
        }
    
        return null;
    }
}