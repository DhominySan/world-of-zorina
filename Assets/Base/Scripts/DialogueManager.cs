using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

	public Image characterIcon;
	public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    public Image additionalImage;

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

		characterIcon.sprite = currentLine.character.icon;
		characterName.text = currentLine.character.name;

		additionalImage.sprite = currentLine.additionalImage;

		if (currentLine.Secundaria)
		{
			additionalImage.color = Color.white;
			characterIcon.color = new Color(1, 1, 1, 0.5f);
		}
		else
		{
			characterIcon.color = Color.white;
			additionalImage.color = new Color(1, 1, 1, 0.5f);
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
		animator.Play("hide");
	}
}
