using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Unit))]
public class UnitBattleController : MonoBehaviour
{
    [SerializeField] private ToCrowdUnitMover _toCrowdMover;

    private Unit _unit;
    private CrowdBattleController _crowdBattleController;

    public bool IsInBattle { get; private set; }

    private void Awake() => _unit = GetComponent<Unit>();

    private void OnEnable()
    {
        _crowdBattleController = _unit.UnitManager.GetComponent<CrowdBattleController>();
        _crowdBattleController.OnBattleStarted += StartBattle;
        if (_crowdBattleController.EnemyUnitManager) StartBattle(_crowdBattleController.EnemyUnitManager);
    }

    private void OnDisable()
    {
        _crowdBattleController = _unit.UnitManager.GetComponent<CrowdBattleController>();
        _crowdBattleController.OnBattleStarted -= StartBattle;
        EndBattle();
    }

    public void StartBattle(CrowdUnitManager enemyUnitManager)
    {
        IsInBattle = true;
        _toCrowdMover.enabled = false;
        StartCoroutine(BattleCorutine(enemyUnitManager));
    }

    public void EndBattle()
    {
        IsInBattle = false;
        _toCrowdMover.enabled = true;
    }

    private IEnumerator BattleCorutine(CrowdUnitManager enemyUnitManager)
    {
        Unit closestEnemyUnit = null;
        while (enemyUnitManager.Count > 0)
        {
            if (closestEnemyUnit == null || !closestEnemyUnit.isActiveAndEnabled)
                closestEnemyUnit = GetClosestUnit(enemyUnitManager.Units);
            else
                _unit.Movement.AddForceTowards(closestEnemyUnit.transform.position, 10f);
            yield return null;
        }
        EndBattle();
    }

    private Unit GetClosestUnit(List<Unit> enemyUnits)
    {
        Unit closestUnit = null;
        float minDistance = 10f;

        foreach (var enemyUnit in enemyUnits)
        {
            float distanceToUnit = Vector3.Distance(transform.position, enemyUnit.transform.position);
            if (distanceToUnit < minDistance)
                closestUnit = enemyUnit;
        }
        return closestUnit;
    }

}

