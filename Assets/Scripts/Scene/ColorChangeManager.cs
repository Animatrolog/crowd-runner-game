using UnityEngine;
using UnityEngine.Events;

public class ColorChangeManager : MonoBehaviour
{
    [SerializeField] private CrowdColorChanger _crowdColorChanger;
    [SerializeField] private RewardedAdHandler _rewardedAd;
    [SerializeField] private int _adPeriod;
    [SerializeField] private AdImageSwitcher _adImageSwitcher;

    private UiSoundManager _uISoundManager; 
    private GameDataManager _gameDataManager;
    private AdCounters _adCounters;
    private bool _isAd;

    public UnityAction<Color> OnColorChange;
    public static ColorChangeManager Instance;
    public Color CrowdColor { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _adCounters = AdCounters.Instance;
        _uISoundManager = UiSoundManager.Instance;
        _gameDataManager = GameDataManager.Instance;
        LoadColor();
        CheckForAd();
    }

    private void LoadColor()
    {
        Color color = _gameDataManager.CrowdColor;
        _crowdColorChanger.SetColor(color);
        CrowdColor = color;
        OnColorChange?.Invoke(color);
    }

    private void CheckForAd()
    {
        _isAd = _adCounters.ColorRewardedAdCounter % _adPeriod == 0;
        _adImageSwitcher.SetRewarded(_isAd, 0);
    }

    public void TryToChangeColor()
    {
        if (_isAd) _rewardedAd.ShowRewardedAd();
        else UpdateColor();
    }

    public void UpdateColor()
    {
        CrowdColor = GetRandomColor();
        _crowdColorChanger.SetColor(CrowdColor);
        _gameDataManager.CrowdColor = CrowdColor;
        _uISoundManager.PlayColorChange();
        _adCounters.ColorRewardedAdCounter++;
        CheckForAd();
        OnColorChange?.Invoke(CrowdColor);
    }

    public Color GetRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color (r, g, b, 1f);
    }
}
