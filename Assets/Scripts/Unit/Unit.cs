using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UnitMovement))]
public class Unit : MonoBehaviour
{
    public event UnityAction OnUnitDeath;
    public CrowdUnitManager UnitManager { get; private set; }
    public UnitMovement Movement { get; private set; }

    private void Awake() => Movement = GetComponent<UnitMovement>();

    public void Initiate(CrowdUnitManager unitManager)
    {
        UnitManager = unitManager;
        gameObject.SetActive(true);
    }

    public void Death()
    {
        OnUnitDeath?.Invoke();
    }
}
