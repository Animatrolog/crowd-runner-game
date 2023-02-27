using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static readonly string _path = Application.persistentDataPath + "/GameData.ccr";

    public static void SaveData (GameDataManager gameDataManager)
    {
        BinaryFormatter formater = new ();
        FileStream fileStream = new (_path, FileMode.Create);
        GameSaveData gameSaveData = new (gameDataManager);
        formater.Serialize(fileStream, gameSaveData);
        fileStream.Close();
    }

    public static GameSaveData LoadData ()
    {
        if (File.Exists(_path))
        {
            BinaryFormatter formater = new ();
            FileStream fileStream = new (_path, FileMode.Open);
            
            GameSaveData gameSaveData = formater.Deserialize(fileStream) as GameSaveData;
            fileStream.Close();
            return gameSaveData;
        }
        else
        {
            Debug.LogWarning("Save file not found !");
            return null;
        }
    }
}
