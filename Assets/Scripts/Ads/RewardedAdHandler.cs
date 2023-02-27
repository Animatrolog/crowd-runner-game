using UnityEngine;
using UnityEngine.Events;

public class RewardedAdHandler : MonoBehaviour
{
    [SerializeField] private string _placementName;
    [SerializeField] private UnityEvent _onRewardEarned;

    private GameDataManager _gameDataManager;

    private void OnEnable() => IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;

    private void OnDisable() => IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;

    private void Start()
    {
        _gameDataManager = GameDataManager.Instance;
    }

    public void ShowRewardedAd()
    {
        if (!_gameDataManager.IsAdsEnabled)
        {
            _onRewardEarned?.Invoke();
            return;
        }

        if (IronSource.Agent.isRewardedVideoAvailable())
            IronSource.Agent.showRewardedVideo(_placementName);
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        string placementName = ssp.getPlacementName();

        if (placementName == _placementName)
            _onRewardEarned?.Invoke();
    }
}
