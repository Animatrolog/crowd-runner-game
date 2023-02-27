using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private CrowdUnitManager _unitManager;
    [SerializeField] private int _oneLevelPrice;
    [SerializeField] private int _upgradePeriod;
    [SerializeField] private AdImageSwitcher _adImageSwitcher;
    [SerializeField] private UpgradeBlock _upgradeBlock;

    private int _count = 1;
    private int _upgradePrice;
    private GameDataManager _gameDataManager;
    private UiSoundManager _uISoundManager;

    private void Start()
    {
        _uISoundManager = UiSoundManager.Instance;
        _gameDataManager = GameDataManager.Instance;
        LoadStartCount();
    }
    
    public void CheckCount()
    {
        _upgradePrice = _count * _oneLevelPrice;

        bool hasEnoughCoins = _gameDataManager.Coins >= _upgradePrice;
        int requiredLevel = _count * _upgradePeriod;
        bool hasEnoughLevel = _gameDataManager.Level >= requiredLevel;

        _adImageSwitcher.SetRewarded(false, _upgradePrice);
        _upgradeBlock.SetBlock(hasEnoughCoins, hasEnoughLevel, requiredLevel);
    }

    public void UpgradeStartCount()
    {
        _gameDataManager.Coins -= _upgradePrice;
        _count++;
        _unitManager.Count = _count;
        _gameDataManager.UnitCount = _count;
        _uISoundManager.PlayUpgrade();
        CheckCount();
    }

    private void LoadStartCount()
    {
        _count = _gameDataManager.UnitCount;
        _unitManager.Count = _count;
        CheckCount();
    }
}
