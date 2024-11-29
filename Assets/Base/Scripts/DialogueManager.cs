using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

	public Image characterIcon;
    public Image npcIcon;
	public TextMeshProUGUI characterName;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI dialogueArea;
    private Queue<DialogueLine> lines;
	public bool isDialogueActive = false;
	public float typingSpeed = 0.2f;
	public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

		lines = new Queue<DialogueLine>();
    }

	public void StartDialogue(Dialogue dialogue)
	{
		isDialogueActive = true;

		DisablePlayerScripts();

		animator.Play("show");

		lines.Clear();

		foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
		{
			lines.Enqueue(dialogueLine);
		}

		DisplayNextDialogueLine();
	}

	public void DisplayNextDialogueLine()
	{
		if (lines.Count == 0)
		{
			EndDialogue();
			return;
		}

		DialogueLine currentLine = lines.Dequeue();

		characterIcon.sprite = currentLine.characters.icon;
		characterName.text = currentLine.characters.name;

		npcIcon.sprite = currentLine.characters.npcIcon;
		npcName.text = currentLine.characters.npcName;

		if (currentLine.NPCSpeaking)
		{
			characterName.color = new Color(1, 1, 1, 0.5f);
			npcName.color = Color.white;

			npcIcon.color = Color.white;
			characterIcon.color = new Color(1, 1, 1, 0.5f);
		}
		else
		{
			characterName.color = Color.white;
			npcName.color = new Color(1, 1, 1, 0.5f);

			characterIcon.color = Color.white;
			npcIcon.color = new Color(1, 1, 1, 0.5f);
		}

		StopAllCoroutines();

		StartCoroutine(TypeSentence(currentLine));
	}

	IEnumerator TypeSentence(DialogueLine dialogueLine)
	{
		dialogueArea.text = "";
		foreach (char letter in dialogueLine.line.ToCharArray())
		{
			dialogueArea.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
	}

	void EndDialogue()
	{
		isDialogueActive = false;

		EnablePlayerScripts();

		animator.Play("hide");
	}

	private void DisablePlayerScripts()
	{
		PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
		CharacterController2D characterController = FindObjectOfType<CharacterController2D>();
		Attack attack = FindObjectOfType<Attack>();

		if (playerMovement != null) playerMovement.enabled = false;
		if (characterController != null) characterController.enabled = false;
		if (attack != null) attack.enabled = false;
	}

	private void EnablePlayerScripts()
	{
		PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
		CharacterController2D characterController = FindObjectOfType<CharacterController2D>();
		Attack attack = FindObjectOfType<Attack>();

		if (playerMovement != null) playerMovement.enabled = true;
		if (characterController != null) characterController.enabled = true;
		if (attack != null) attack.enabled = true;
	}
}
