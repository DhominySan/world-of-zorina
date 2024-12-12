using UnityEngine;

public class SaveLoadButton : MonoBehaviour
{
    public GameManager gameManager; // Referência ao GameManager
    public int slot; // Número do slot (definido no Inspector)

    // Chamado pelo botão para salvar
    public void Save()
    {
        if (gameManager != null)
        {
            gameManager.SaveGame(slot);
        }
    }

    // Chamado pelo botão para carregar
    public void Load()
    {
        if (gameManager != null)
        {
            gameManager.LoadGame(slot);
        }
    }
}
