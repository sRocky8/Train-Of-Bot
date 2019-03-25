using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueClass {

    public string npcName;

    public DialogueAmount[] dialogueAmount;

}

[System.Serializable]
public class DialogueAmount
{
    [TextArea(3, 3)]
    public string parameterDescription;

    [TextArea(3, 10)]
    public string[] linesOfDialogue;
}