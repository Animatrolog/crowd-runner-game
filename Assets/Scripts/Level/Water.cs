using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject _particles;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Unit>(out Unit unit))
        {
            Vector3 unitPosition = unit.transform.position;
            Instantiate(_particles, new Vector3(unitPosition.x, transform.position.y, unitPosition.z), Quaternion.identity);
        }
    }
}
