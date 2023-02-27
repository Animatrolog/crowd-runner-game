using UnityEngine;

public static class GameVibration
{
    private static GameDataManager _gameDataManager;

    public static void Initialize()
    {
        _gameDataManager = GameDataManager.Instance;
    }

    public static void Vibrate()
    {
        if(_gameDataManager.IsVibrationEnabled)
        {
            Handheld.Vibrate();
        }
    }

}