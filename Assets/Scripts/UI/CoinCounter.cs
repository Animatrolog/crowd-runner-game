using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;

    public UnityEvent<int> OnCountUpdated;

    private GameDataManager _gameDataManager;

    private void Start()
    {
        _gameDataManager = GameDataManager.Instance;
        LoadData();
        _gameDataManager.OnCoinCountChange += UpdateCounter;
    }

    private void LoadData()
    {
        if (_gameDataManager != null)
            UpdateCounter(_gameDataManager.Coins);
    }

    private void UpdateCounter(int count)
    {
        _counterText.text = count.ToString();
        OnCountUpdated?.Invoke(count);
    }
}
