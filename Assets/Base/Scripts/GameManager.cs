using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public void SaveGame()
    {
        string currentRoom = SceneManager.GetActiveScene().name;
        SaveData data = new SaveData
        {
            roomName = currentRoom // Obtenha o nome da sala atual
        };

        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            Debug.Log("Carregado na sala: " + data.roomName);
        }
    }
}
