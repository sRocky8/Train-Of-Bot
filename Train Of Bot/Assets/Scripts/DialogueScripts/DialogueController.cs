using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour {

    //Public Variables
    public Text npcNameText;
    public Text npcDialogueText;
    public GameObject textBox;
    [HideInInspector]public bool endedDialogue;

    //Private Variables
    private Queue<string> linesOfDialogue;
//    private int currentScene;
//    private int lastScene;
    private GameObject npcNameTextGameObject;
    private GameObject npcDialogueTextGameObject;

    void Start () {
        linesOfDialogue = new Queue<string>();

        endedDialogue = true;
//        currentScene = SceneManager.GetActiveScene().buildIndex;
//        lastScene = currentScene;
    }

    void Update()
    {
//        currentScene = SceneManager.GetActiveScene().buildIndex;
//        lastScene = currentScene;
    }

    public void StartDialogue(DialogueClass npcDialogue, int dialogueParameter)
    {
        endedDialogue = false;
        textBox.SetActive(true);
        npcNameText.text = npcDialogue.npcName;
        linesOfDialogue.Clear();
        foreach (string line in npcDialogue.dialogueAmount[dialogueParameter].linesOfDialogue)
        {
            linesOfDialogue.Enqueue(line);
        }

        ShowNextLine();
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
