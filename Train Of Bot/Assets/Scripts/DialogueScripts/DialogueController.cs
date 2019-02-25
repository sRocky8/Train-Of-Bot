using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    //Public Variables
    public Text npcNameText;
    public Text npcDialogueText;
    public GameObject textBox;
    public bool endedDialogue;

    //Private Variables
    private Queue<string> linesOfDialogue;


	void Start () {
        linesOfDialogue = new Queue<string>();
        endedDialogue = true;
	}

    public void StartDialogue(DialogueClass npcDialogue)
    {
        endedDialogue = false;
        textBox.SetActive(true);
        npcNameText.text = npcDialogue.npcName;
        linesOfDialogue.Clear();
        foreach (string line in npcDialogue.linesOfDialogue)
        {
            linesOfDialogue.Enqueue(line);
        }
    }

    public void ShowNextLine()
    {
        if (linesOfDialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = linesOfDialogue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(LineByCharacter(line));
    }

    IEnumerator LineByCharacter(string line)
    {
        npcDialogueText.text = "";
        foreach(char character in line.ToCharArray())
        {
            npcDialogueText.text += character;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        textBox.SetActive(false);
        endedDialogue = true;
    }
}
