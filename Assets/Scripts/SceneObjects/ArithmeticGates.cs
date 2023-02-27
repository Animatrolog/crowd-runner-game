using UnityEngine;

public class ArithmeticGates : MonoBehaviour
{
    [SerializeField] private Arithmetiser _leftGate;
    [SerializeField] private Arithmetiser _rightGate;
    [SerializeField] private BoxCollider _boxCollider;

    public Arithmetiser LeftGate => _leftGate;
    public Arithmetiser RightGate => _rightGate;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CrowdUnitManager>(out CrowdUnitManager unitManager))
        {
            Vector3 unitPosition = unitManager.transform.position;
            float distanceToLeftGate = Vector3.Distance(_leftGate.transform.position, unitPosition);
            float distanceToRightGate = Vector3.Distance(_rightGate.transform.position, unitPosition);

            if (distanceToLeftGate >= distanceToRightGate)
            {
                unitManager.Count = _rightGate.Calculate(unitManager.Count);
                _rightGate.gameObject.SetActive(false);
            }
            else
            {
                unitManager.Count = _leftGate.Calculate(unitManager.Count);
                _leftGate.gameObject.SetActive(false);
            }
            _boxCollider.enabled = false;
        }
    }
}
