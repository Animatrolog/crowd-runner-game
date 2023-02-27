using UnityEngine;

public class OnStateEnabler : MonoBehaviour
{
    [SerializeField] private GameState _gameStateToEnable;
    [SerializeField] private bool _disableOnDifferentState;
    [SerializeField] private GameObject _objectToEnable;

    private void OnEnable() => GameStateManager.OnStateChange += ManageScripts;

    private void OnDisable() => GameStateManager.OnStateChange -= ManageScripts;

    private void ManageScripts(GameState state)
    {
        if (state == _gameStateToEnable)
            _objectToEnable.SetActive(true);
        else if (_disableOnDifferentState)
            _objectToEnable.SetActive(false);
    }
}
