using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(CrowdRadiusCalculator))]
public class CrowdUnitManager : MonoBehaviour
{
    [SerializeField] Unit _unitPrefab;
    [SerializeField] [Min(1)] private int _maxUnitCount = 250;
    [SerializeField] [Min (1)] private int _count = 1;

    private ObjectPool<Unit> _poolOfUnits;
    private CrowdRadiusCalculator _radiusCalculator;

    public List<Unit> Units { get; private set; } = new();
    public float CrowdRadius { get; private set; }

    public event UnityAction<int> OnCountChanged;
    public event UnityAction OnUnitSpawned;
    public event UnityAction OnUnitDestroyed;
    public event UnityAction OnAllUnitsDestroyed;

    public int Count
    {
        get =>_count;

        set
        {
            _count = Mathf.Clamp(value, 0, _maxUnitCount);
            AnnihilationCheck();
            CrowdRadius = _radiusCalculator.Calculate(_count + 1);
            if (Units.Count < _count)
                SpawnUnits();
            OnCountChanged?.Invoke(_count);
        }
    }

    private void Awake()
    {
        _poolOfUnits = new ObjectPool<Unit>(_unitPrefab, _maxUnitCount, transform);
        _radiusCalculator = GetComponent<CrowdRadiusCalculator>();
    }

    private void Start()
    {
        SpawnUnits();
    }

    void AnnihilationCheck()
    {
        if (Count < 1)
        {
            OnAllUnitsDestroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void SpawnUnits()
    {
        while (Units.Count < Count)
        {
            SpawnUnit();
        }
        OnUnitSpawned?.Invoke();
    }

    private void SpawnUnit()
    {
        Vector3 randomOffset = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward * Random.Range(0f, CrowdRadius);
        Unit unit = _poolOfUnits.GetFreeObject();
        unit.transform.position = transform.position + randomOffset;
        unit.Initiate(this);
        Units.Add(unit);
    }

    public void KillUnit(Unit unit)
    {
        if (Count <= 0 || !Units.Contains(unit)) return;
       
        Units.Remove(unit);
        unit.Death();
        unit.gameObject.SetActive(false);
        OnUnitDestroyed?.Invoke();
        Count--;
    }
}
