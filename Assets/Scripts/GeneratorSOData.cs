using UnityEngine;

[CreateAssetMenu(fileName = "NewGeneratorData", menuName = "Generators/GeneratorData")]
public class GeneratorSOData : ScriptableObject
{
    [System.Serializable]
    public class ProductProbability
    {
        public Unit Unit;
        [Range(0f, 100f)]
        public float Probability;
    }

    [SerializeField] private ProductProbability[] _productProbabilities;
    
    public Unit SelectObject()
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
                return objProb.Unit;
            }
        }
    
        // Döngüden çıkıldığında herhangi bir obje döndürülmezse null döndür
        return null;
    }
}