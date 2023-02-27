using UnityEngine;

[RequireComponent(typeof(CrowdUnitManager))]
public class VibrationHandler : MonoBehaviour
{

    private CrowdUnitManager _unitManager;

    private void Awake() => _unitManager = GetComponent<CrowdUnitManager>();

    private void OnEnable()
    {
        _unitManager.OnUnitDestroyed += OnDeath;
        _unitManager.OnUnitSpawned += OnSpawn;
        GameVibration.Initialize();
    }

    private void OnDisable()
    {
        _unitManager.OnUnitDestroyed -= OnDeath;
        _unitManager.OnUnitSpawned -= OnSpawn;
    }

    private void OnSpawn()
    {
        GameVibration.Vibrate();
    }

    private void OnDeath()
    {
        GameVibration.Vibrate();
    }
}
