using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _inGameUi;
    [SerializeField] private GameObject _pausePanel;

    private GameObject _activePannel;
    private List<GameObject> _unactivePanels;

    private void OnEnable() => GameStateManager.OnStateChange += ManagePanels;

    private void OnDisable() => GameStateManager.OnStateChange -= ManagePanels;

    private void Awake()
    {
        _unactivePanels = new List<GameObject> { _startPanel, _gameoverPanel, _finishPanel, _inGameUi, _pausePanel};
        HideAllPanels();
    }

    private void ManagePanels(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                SetActivePannel(_startPanel);
                break;
            case GameState.Game:
                SetActivePannel(_inGameUi);
                break;
            case GameState.Defeat:
                SetActivePannel(_gameoverPanel);
                break;
            case GameState.Finish:
                SetActivePannel(_finishPanel);
                break;
            case GameState.Pause:
                SetActivePannel(_pausePanel);
                break;
        }   
    }

    private void SetActivePannel(GameObject panel)
    {
        DisableActivePanel();
        _unactivePanels.Remove(panel);
        _activePannel = panel;
        panel.SetActive(true);
    }

    private void HideAllPanels()
    {
        DisableActivePanel();
        foreach (GameObject panel in _unactivePanels)
            panel.SetActive(false);
    }

    private void DisableActivePanel()
    {
        if (_activePannel == null) return;
        _unactivePanels.Add(_activePannel);
        _activePannel.SetActive(false);
        _activePannel = null;
    }
}
