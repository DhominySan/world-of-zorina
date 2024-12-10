using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour
{
    public void LoadSavedRoom()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            SceneManager.LoadScene(data.roomName);
        }
        else
        {
            Debug.LogWarning("Nenhum save encontrado!");
        }
    }
}
