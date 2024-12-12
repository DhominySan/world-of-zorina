using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public void SaveGame(int slot)
    {
        string currentRoom = SceneManager.GetActiveScene().name;
        SaveData data = new SaveData
        {
            roomName = currentRoom // Salva a sala atual.
        };

        SaveSystem.SaveGame(data, slot);
    }

    public void LoadGame(int slot)
    {
        SaveData data = SaveSystem.LoadGame(slot);
        if (data != null)
        {
            Debug.Log("Carregado do slot " + slot + " na sala: " + data.roomName);
            SceneManager.LoadScene(data.roomName); // Carrega a sala salva
        }
        else
        {
            Debug.LogWarning("Nenhum save encontrado no slot " + slot);
        }
    }
}
