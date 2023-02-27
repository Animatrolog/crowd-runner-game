using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(UnitBattleController))]
public class EnemyUnitKiller : MonoBehaviour
{
    private Unit _unit;
    private UnitBattleController _battleController;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _battleController = GetComponent<UnitBattleController>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!_battleController.IsInBattle) return;

        if (collision.gameObject.TryGetComponent<Unit>(out Unit enemyUnit))
        {
            if (_unit.UnitManager == enemyUnit.UnitManager || !enemyUnit.isActiveAndEnabled) return;

            CrowdUnitManager enemyUnitManager = enemyUnit.UnitManager;
            enemyUnitManager.KillUnit(enemyUnit);
            _unit.UnitManager.KillUnit(_unit);
        }
    }

}
