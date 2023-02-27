using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState _defaultState;

    public static GameStateManager Instance { get; private set; }
    public static GameState CurrentGameState { get; private set; }

    public static event UnityAction<GameState> OnStateChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    
    void Start()
    {
        SetState(_defaultState);
    }

    public void SetState(GameState state)
    {
        CurrentGameState = state;
        OnStateChange?.Invoke(state);
        Debug.Log(state.ToString());
    }
}

public enum GameState
{
    Menu = 0,
    Game = 1,
    Defeat = 2,
    Finish = 3,
    Pause = 4
}

