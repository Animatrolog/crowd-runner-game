using UnityEngine;

public class CrowdRadiusCalculator : MonoBehaviour
{
    [SerializeField] private float _unitColliderRadius = 0.18f;

    const float OneDividedByPI = 1 / Mathf.PI;
    private float _areaOfUnit;
    
    private void Awake()
    {
        _areaOfUnit = (_unitColliderRadius * 2) * (_unitColliderRadius * 2) ;    
    }

    public float Calculate(int unitCount)
    {
        float areaOfUnits = _areaOfUnit * unitCount;
        return Mathf.Sqrt(areaOfUnits * OneDividedByPI);
    }


}
