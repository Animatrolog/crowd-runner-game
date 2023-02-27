using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float _blastRadius;
    [SerializeField] private float _lethalRadius;
    [SerializeField] private float _blastForce;
    [SerializeField] private bool _disableAfterExplosion;
    [SerializeField] private GameObject _particles;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<UnitMovement>(out UnitMovement unit))
        {
            Damage();
            Explode();
            if(_disableAfterExplosion) 
                gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, _blastRadius);
        foreach (Collider nearbyCollider in nearbyColliders)
        {
            if (nearbyCollider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_blastForce, transform.position, _blastRadius);
        }
        Instantiate(_particles, transform.position, Quaternion.identity);
    }

    private void Damage()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, _lethalRadius);
        foreach (Collider nearbyCollider in nearbyColliders)
        {
            if (nearbyCollider.TryGetComponent<Unit>(out Unit unit))
                        unit.UnitManager.KillUnit(unit);
        }
    }
}
