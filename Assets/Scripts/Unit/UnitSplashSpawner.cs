using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(UnitGravity))]
public class UnitSplashSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _particles;

    private Unit _unit;
    private UnitGravity _unitGravity;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitGravity = GetComponent<UnitGravity>();
    }

    private void OnEnable()
    {
        _unit.OnUnitDeath += SpawnSplash;
        ResetParticles();
    }

    private void OnDisable() => _unit.OnUnitDeath -= SpawnSplash;

    private void SpawnSplash()
    {
        if (_unitGravity.IsGrounded)
        {
            _particles.transform.parent = null;
            _particles.SetActive(true);    
        }
    }

    private void ResetParticles()
    {
        _particles.transform.parent = transform;
        _particles.transform.position = transform.position;
        _particles.SetActive(false);
    }
}
