using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/gameSave.dat";

    public static void SaveGame(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
        Debug.Log("Jogo salvo em: " + savePath);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(savePath, FileMode.Open))
            {
                SaveData data = formatter.Deserialize(stream) as SaveData;
                Debug.Log("Jogo carregado.");
                return data;
            }
        }
        else
        {
            Debug.LogWarning("Arquivo de save não encontrado.");
            return null;
        }
    }
}
