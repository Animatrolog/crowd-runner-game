using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameStateManager _gameStateManager;

    void Start()
    {
        _gameStateManager = GameStateManager.Instance;
    }

    public void PauseGame()
    {
        _gameStateManager.SetState(GameState.Pause);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _gameStateManager.SetState(GameState.Game);
        Time.timeScale = 1f;
    }
}
