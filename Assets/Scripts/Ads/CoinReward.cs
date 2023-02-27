using UnityEngine;

public class CoinReward : MonoBehaviour
{
    [SerializeField] private int _coinReward;

    private GameDataManager _gameDataManager;

    private void Start()
    {
        _gameDataManager = GameDataManager.Instance;
    }

    public void GetReward()
    {
        _gameDataManager.Coins += _coinReward;
    }
}
