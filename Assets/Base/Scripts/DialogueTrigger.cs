using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public string npcName;
    public Sprite icon;
    public Sprite npcIcon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter characters;
    [TextArea(3, 10)]
    public string line;
    public bool NPCSpeaking;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não está inicializado!");
            return;
        }

        if (dialogue == null)
        {
            Debug.LogError("Diálogo não atribuído!");
            return;
        }

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            TriggerDialogue();
        }
    }

    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            Debug.LogError("Collider é null!");
            return;
        }

        if(collision.tag == "Player")
        {
            isPlayerInTrigger = true; // Marca que o jogador está na área de colisão
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player")
        {
            isPlayerInTrigger = false; // Marca que o jogador saiu da área de colisão
        }
    }
}
