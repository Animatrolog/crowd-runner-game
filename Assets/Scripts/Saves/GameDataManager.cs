using UnityEngine;
using UnityEngine.Events;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private Color _crowdColor;
    [SerializeField] private int _unitCount;
    [SerializeField] private int _level;
    [SerializeField] private bool _isVibrationEnabled;
    [SerializeField] private bool _isSoundEnabled;
    [SerializeField] private bool _isAdsEnabled;
    [SerializeField] private int _coins;

    public static GameDataManager Instance;
    public int Gems;
    public UnityAction<int> OnCoinCountChange;

    public bool IsVibrationEnabled { get => _isVibrationEnabled; set { _isVibrationEnabled = value; SaveGameData(); }}
    public bool IsSoundEnabled { get => _isSoundEnabled; set { _isSoundEnabled = value; SaveGameData(); }}
    public bool IsAdsEnabled { get => _isAdsEnabled; set { _isAdsEnabled = value; SaveGameData(); }}
    public int Level { get => _level; set{ _level = value; SaveGameData(); }}
    public int UnitCount { get => _unitCount; set { _unitCount = value; SaveGameData(); }}
    public int Coins { get => _coins; set { _coins = value; SaveGameData(); OnCoinCountChange?.Invoke(value); } }
    public Color CrowdColor { get => _crowdColor; set { _crowdColor = value; SaveGameData(); }}

    private void Awake()
    {
        LoadGameData();
        Instance = this;
    }

    private void LoadGameData()
    {
        GameSaveData gameSaveData =  SaveSystem.LoadData();
        if (gameSaveData == null) return;

        float[] colorArray = gameSaveData.CrowdColor;
        CrowdColor = new(colorArray[0], colorArray[1], colorArray[2], 1f);

        Level = gameSaveData.Level;
        UnitCount = gameSaveData.UnitCount;
        Coins = gameSaveData.Coins;
        Gems = gameSaveData.Gems;

        IsVibrationEnabled = gameSaveData.IsVibrationEnabled;
        IsSoundEnabled = gameSaveData.IsSoundEnabled;
        IsAdsEnabled = gameSaveData.IsAdsEnabled;
    }

    public void SaveGameData()
    {
        SaveSystem.SaveData(this);
    }

}
