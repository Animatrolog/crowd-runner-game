using UnityEngine;

public class UnitDestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Unit>(out Unit unit))
        {
            unit.UnitManager.KillUnit(unit);
        }
    }
}
