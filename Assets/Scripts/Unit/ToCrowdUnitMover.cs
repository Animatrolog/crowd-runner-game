using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ToCrowdUnitMover : MonoBehaviour
{
    [SerializeField] private float _maxForceMagnitude;
    private Unit _unit;
    private UnitMovement _unitMovement;
    private Transform _unitManagerTransform;

    private void OnEnable()
    {
        _unit = GetComponent<Unit>();
        _unitMovement = GetComponent<UnitMovement>();
    }

    void Start()
    {
        _unitManagerTransform = _unit.UnitManager.transform;
    }

    void FixedUpdate()
    {
        _unitMovement.AddForceTowards(_unitManagerTransform.position, _maxForceMagnitude);
    }

}
