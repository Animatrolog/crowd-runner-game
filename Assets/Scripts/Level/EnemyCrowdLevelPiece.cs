using UnityEngine;

public class EnemyCrowdLevelPiece : LevelPieace
{
    [SerializeField] private CrowdUnitManager _crowdUnitManager;
    [SerializeField] private float _maxCountRatio;

    private int _enemyUnitcount;

    public override int Initialize(int level, int index, int unitCount = 1)
    {
        base.Initialize(level, index, unitCount);
        ApplyEnemyUnitCount();
        return GetLargestPotentialCount(unitCount);
    }

    private int GetLargestPotentialCount(int unitCount)
    {
        return unitCount - _enemyUnitcount;
    }

    private void ApplyEnemyUnitCount()
    {
        _enemyUnitcount = (int)Random.Range(0, base.UnitCount * _maxCountRatio);
        _crowdUnitManager.Count = _enemyUnitcount;
    }
}
