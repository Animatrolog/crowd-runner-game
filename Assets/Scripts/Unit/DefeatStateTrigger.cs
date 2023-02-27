using UnityEngine;

[RequireComponent(typeof(CrowdUnitManager))]
public class DefeatStateTrigger : MonoBehaviour
{
    private CrowdUnitManager _unitManager;

    private void Awake() => _unitManager = GetComponent<CrowdUnitManager>();

    private void OnEnable() => _unitManager.OnAllUnitsDestroyed += TriggerDefeatState;

    private void TriggerDefeatState() => GameStateManager.Instance.SetState(GameState.Defeat);
}
