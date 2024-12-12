using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/gameSave";

    // Salva os dados em um slot específico (1, 2 ou 3)
    public static void SaveGame(SaveData data, int slot)
    {
        string filePath = savePath + slot + ".dat"; // Exemplo: gameSave1.dat, gameSave2.dat, etc.
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
        Debug.Log("Jogo salvo no slot " + slot + " em: " + filePath);
    }

    // Carrega os dados de um slot específico (1, 2 ou 3)
    public static SaveData LoadGame(int slot)
    {
        string filePath = savePath + slot + ".dat";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                SaveData data = formatter.Deserialize(stream) as SaveData;
                Debug.Log("Jogo carregado do slot " + slot);
                return data;
            }
        }
        else
        {
            Debug.LogWarning("Arquivo de save não encontrado no slot " + slot);
            return null;
        }
    }
}
