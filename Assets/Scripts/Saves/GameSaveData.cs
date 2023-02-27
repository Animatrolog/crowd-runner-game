[System.Serializable]
public class GameSaveData
{
    public float[] CrowdColor = new float[3];
    public int Level;
    public int UnitCount;
    public int Coins;
    public int Gems;
    public bool IsVibrationEnabled;
    public bool IsSoundEnabled;
    public bool IsAdsEnabled;

    public GameSaveData(GameDataManager gameDataManager)
    {
        CrowdColor[0] = gameDataManager.CrowdColor.r;
        CrowdColor[1] = gameDataManager.CrowdColor.g;
        CrowdColor[2] = gameDataManager.CrowdColor.b;

        UnitCount = gameDataManager.UnitCount;
        Level = gameDataManager.Level;
        Coins = gameDataManager.Coins;
        Gems = gameDataManager.Gems;

        IsVibrationEnabled = gameDataManager.IsVibrationEnabled;
        IsSoundEnabled = gameDataManager.IsSoundEnabled;
        IsAdsEnabled = gameDataManager.IsAdsEnabled;
    }
}
