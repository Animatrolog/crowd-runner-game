using UnityEngine;

public class AdBanner : MonoBehaviour
{  
    private GameDataManager _gameDataManager;

    public void Start()
    {
        _gameDataManager = GameDataManager.Instance;
        RequestBannerAd();
        if (!_gameDataManager && !_gameDataManager.IsAdsEnabled)
            DestroyBannerAd();
    }

    public void RequestBannerAd()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    public void DestroyBannerAd()
    {
        IronSource.Agent.destroyBanner();
    }
}
