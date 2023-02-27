using UnityEngine;

[RequireComponent(typeof(CrowdUnitManager))]
public class CrowdAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _deathClip;
    
    private CrowdUnitManager _unitManager;

    private void Awake() => _unitManager = GetComponent<CrowdUnitManager>();

    private void OnEnable()
    {
        _unitManager.OnUnitDestroyed += OnDeath;
        _unitManager.OnUnitSpawned += OnSpawn;
    }

    private void OnDisable()
    {
        _unitManager.OnUnitDestroyed -= OnDeath;
        _unitManager.OnUnitSpawned -= OnSpawn;
    }

    private void OnSpawn()
    {     
        _audioSource.pitch = Random.Range(.8f, 1.2f);
        _audioSource.PlayOneShot(_deathClip);
    }

    private void OnDeath()
    {
        _audioSource.pitch = Random.Range(.8f, 1.2f);
        _audioSource.PlayOneShot(_deathClip);
    }
}
