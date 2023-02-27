using UnityEngine;

public class DeadlyHummer : MonoBehaviour
{
    [SerializeField] private float _blastRadius;
    [SerializeField] private float _blastForce;
    [SerializeField] private Transform _blastPoint;
    [SerializeField] private GameObject _particles;
    [SerializeField] private AudioSource _audioSource;

    private void PlaySound()
    {
        _audioSource.Play();
    }

    private void Punch()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(_blastPoint.position, _blastRadius);
        foreach (Collider nearbyCollider in nearbyColliders)
        {
            if (nearbyCollider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_blastForce, _blastPoint.position, _blastRadius);
        }
        Instantiate(_particles, _blastPoint.position, Quaternion.identity);
    }
}
