using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitRotation : MonoBehaviour
{
    [SerializeField] Transform _meshTransform;

    private Unit _unit;
    private CrowdMovement _crowdMovement;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _crowdMovement = _unit.UnitManager.GetComponent<CrowdMovement>();
    }

    private void Update()
    {
        _meshTransform.rotation = Quaternion.LookRotation(_unit.Movement.Velocity + _crowdMovement.CrowdDirection.normalized);
    }
}
