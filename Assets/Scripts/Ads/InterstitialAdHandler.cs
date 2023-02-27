using UnityEngine;

public class InterstitialAdHandler : MonoBehaviour
{
    [SerializeField] private int _adPeriod; 
    
    private AdCounters _adCounters;
    private GameDataManager _gameDataManager;

    public static InterstitialAdHandler Instance;

    void OnApplicationPause(bool isPaused) => IronSource.Agent.onApplicationPause(isPaused);

    private void Awake()
    {
        LoadAd();
        Instance = this;
    }

    private void Start()
    {
        _adCounters = AdCounters.Instance;
        _gameDataManager = GameDataManager.Instance;      
    }

    private void LoadAd()
    {
        IronSource.Agent.loadInterstitial();
    }

    public void ShowAd()
    {
        if (!_gameDataManager.IsAdsEnabled) return;
            _adCounters.InterstitialAdCounter++;

        if (_adCounters.InterstitialAdCounter % _adPeriod > 0) return;

        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
    }
}
