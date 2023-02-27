using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CrowdUnitManager))]
public class CrowdBattleController : MonoBehaviour
{
    [SerializeField] private GameObject _inputHandler;
    
    private CrowdUnitManager _unitManager;

    public UnityAction<CrowdUnitManager> OnBattleStarted;

    private void Awake() => _unitManager = GetComponent<CrowdUnitManager>();

    private void OnEnable() => _unitManager.OnAllUnitsDestroyed += EndBattle;

    private void OnDisable() => _unitManager.OnAllUnitsDestroyed -= EndBattle;
    
    public CrowdUnitManager EnemyUnitManager { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CrowdUnitManager>(out CrowdUnitManager enemyUnitManager))
        {
            StartBattle(enemyUnitManager);
        }
    }

    private void StartBattle(CrowdUnitManager enemyUnitManager)
    {
        EnemyUnitManager = enemyUnitManager;
        if(_inputHandler != null) _inputHandler.SetActive(false);
        enemyUnitManager.OnAllUnitsDestroyed += EndBattle;
        OnBattleStarted?.Invoke(enemyUnitManager);   
    }

    private void EndBattle()
    {
        EnemyUnitManager = null;
        if (_inputHandler != null) _inputHandler.SetActive(true);
        Debug.Log("EndBattle");
    }
}
