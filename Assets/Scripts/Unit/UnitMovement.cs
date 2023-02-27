using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Unit))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier;

    private Unit _unit;
    private Rigidbody _rigidbody;
    private CrowdMovement _crowdMovement;

    public Vector3 Velocity { get => _rigidbody.velocity; }
    public bool IsRunning => (_crowdMovement.CrowdDirection + _rigidbody.velocity).magnitude > 0.5f;

    private void Awake() 
    {
        _unit = GetComponent<Unit>();
        _rigidbody = GetComponent<Rigidbody>();
        _crowdMovement = _unit.UnitManager.GetComponent<CrowdMovement>();
    }

    public void AddForceTowards(Vector3 targetPosition, float maxForceMagnitude)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection = Vector3.ClampMagnitude(targetDirection, maxForceMagnitude);
        Vector3 targetForce = (targetDirection - _rigidbody.velocity) * _forceMultiplier;
        _rigidbody.AddForce(targetForce);
    }
}
